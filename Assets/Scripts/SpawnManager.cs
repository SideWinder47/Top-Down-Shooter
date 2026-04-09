using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] enemyArray;
	[SerializeField] private GameManager gameManager;
	
	private float spawnInterval = 2.0f;
	private float spawnDelay = 1.0f;
	
	private float xSpawnRange = 23.0f;
	private float ySpawnRange = 0.5f;
	private float zSpawnRange = 11.5f;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    InvokeRepeating("SpawnRandomEnemy", spawnDelay, spawnInterval);
    }

	void SpawnRandomEnemy()
	{
		if (gameManager.isGameActive)
		{
			int randomEnemy = Random.Range(0, enemyArray.Length);
			float randomSpawnPosX = Random.Range(-xSpawnRange, xSpawnRange);
		
			Vector3 spawnPos = new Vector3(randomSpawnPosX, ySpawnRange, zSpawnRange);
		
			Instantiate(enemyArray[randomEnemy], spawnPos, enemyArray[randomEnemy].gameObject.transform.rotation);
		}
		
	}
	
}
