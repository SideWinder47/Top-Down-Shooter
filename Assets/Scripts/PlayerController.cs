using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	private Renderer[] playerRenderers;
	private AudioSource gunSound;
	private GameManager gameManager;
	
	private Vector3 movementInput;
	private float speed = 10.0f;
	private float xBounds = 21.0f;
	private float zBounds = 9.5f;
	
	private float rateOfFire = 0.5f;
	private bool canFire = true;
	[SerializeField] private GameObject bulletSpawn;
	
	private int hit = -1;
	private bool isInvincible = false;
	[SerializeField] private float iFrameTime = 1.5f;
	[SerializeField] private float blinkInterval = 0.15f;
	
	void Awake()
    {
	    playerRb = GetComponent<Rigidbody>();
	    gunSound = GetComponent<AudioSource>();
	    playerRenderers = GetComponentsInChildren<Renderer>();
    }
    
	void Start()
	{
		gameManager = GameManager.Instance;
	}

    void Update()
    {
	    // get input for movement
	    float horizontalMovement = Input.GetAxis("Horizontal");
	    float verticalMovement = Input.GetAxis("Vertical");
	    
	    movementInput = new Vector3(horizontalMovement, 0, verticalMovement);
	    
	    if (Input.GetKeyDown(KeyCode.Space) && canFire && gameManager.isGameActive)
	    {
	    	Fire();
	    }
    }
    
	void Fire()
	{
		gunSound.Play();
		
		// Handles bullet spawn
		GameObject bullet = ObjectPooler.SharedInstance.GetPlayerBullet();
		bullet.transform.position = bulletSpawn.transform.position;
		bullet.transform.rotation = bulletSpawn.transform.rotation;
		
		bullet.SetActive(true);
		
		canFire = false;
		StartCoroutine(FireRate());
	}
    
	void FixedUpdate()
	{
		if (gameManager.isGameActive)
		{
			PlayerMovement();
		}
	}
    
	// Controls how fast the player can fire
	IEnumerator FireRate()
	{
		yield return new WaitForSeconds(rateOfFire);
		canFire = true;
	}
	
	// Controls the time the player is invincible as well as the blinking of the iFrames
	IEnumerator IFrameBlink()
	{
		isInvincible = true;
		float timeElasped = 0;
		
		while (timeElasped < iFrameTime)
		{
			foreach (Renderer r in playerRenderers)
				r.enabled = !r.enabled;
				
			yield return new WaitForSeconds(blinkInterval);
			timeElasped += blinkInterval;
		}
		
		foreach (Renderer r in playerRenderers)
			r.enabled = !r.enabled;
			
		isInvincible = false;
	}
	
	void PlayerMovement()
	{
		// move the player
		playerRb.linearVelocity = movementInput * speed;
		
		// prevent moving offscreen
		Vector3 position = playerRb.position;
		
		position.x = Mathf.Clamp(position.x, -xBounds, xBounds);
		position.z = Mathf.Clamp(position.z, -zBounds, zBounds);
		
		playerRb.position = position;
		
	}
	
	// When hit by bullet
	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Bullet(Enemy)") && !isInvincible)
		{
			other.gameObject.SetActive(false);
			gameManager.UpdateLife(hit);
			if (gameManager.lives == 0)
			{
				gameObject.SetActive(false);
			}
			else
			{
				StartCoroutine(IFrameBlink());
			}
		}
	}
	
	// When player collides with an enemy
	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag("Enemy") && !isInvincible)
		{
			Destroy(other.gameObject);
			gameManager.UpdateLife(hit);
			if (gameManager.lives == 0)
			{
				gameObject.SetActive(false);
			}
			else
			{
				StartCoroutine(IFrameBlink());
			}
		}
	}
	
}
