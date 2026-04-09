using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private GameManager gameManager;
	private GameObject player;
	private float speed = 4.0f;
	public int score;
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    player = GameObject.Find("Player");
	    gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
	    Vector3 lookDirection = (player.transform.position - transform.position).normalized;
	    
	    transform.Translate(lookDirection * speed * Time.deltaTime);
	    
	    if (!gameManager.isGameActive)
	    {
	    	Destroy(gameObject);
	    }
    }
    
	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Bullet(Player)"))
		{
			other.gameObject.SetActive(false);
			gameManager.UpdateScore(score);
			Destroy(gameObject);
		}
	}
}
