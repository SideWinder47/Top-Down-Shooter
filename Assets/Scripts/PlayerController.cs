using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	private GameManager gameManager;
	
	private int hit = -1;
	
	private Vector3 movementInput;
	private float speed = 10.0f;
	private float xBounds = 21.0f;
	private float zBounds = 9.5f;
	
	private float rateOfFire = 0.5f;
	private bool canFire = true;
	[SerializeField] private GameObject bulletSpawn;
	
	private AudioSource gunSound;
	
	void Awake()
    {
	    playerRb = GetComponent<Rigidbody>();
	    gunSound = GetComponent<AudioSource>();
    }
    
	void Start()
	{
		gameManager = GameManager.Instance;
	}

    // Update is called once per frame
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
    
	IEnumerator FireRate()
	{
		yield return new WaitForSeconds(rateOfFire);
		canFire = true;
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
	
	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Bullet(Enemy)"))
		{
			other.gameObject.SetActive(false);
			gameManager.UpdateLife(hit);
			if (gameManager.lives == 0)
			{
				gameObject.SetActive(false);
			}
		}
	}
	
	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Destroy(other.gameObject);
			gameManager.UpdateLife(hit);
			if (gameManager.lives == 0)
			{
				gameObject.SetActive(false);
			}
		}
	}
	
}
