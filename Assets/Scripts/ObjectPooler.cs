using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler SharedInstance;
	
	private List<GameObject> pooledPlayerBullets;
	[SerializeField] private GameObject playerBullet;
	[SerializeField] private int playerBulletPoolAmount;
	
	private List<GameObject> pooledEnemyBullets;
	[SerializeField] private GameObject enemyBullet;
	[SerializeField] private int enemyBulletPoolAmount;

	// Awake is called when the script instance is being loaded
	void Awake()
	{
		SharedInstance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		// Loop through list of pooled objects,deactivating them and adding them to the list 
		pooledPlayerBullets = new List<GameObject>();
		pooledEnemyBullets = new List<GameObject>();
		
		// Player bullets
		for (int i = 0; i < playerBulletPoolAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(playerBullet);
			obj.SetActive(false);
			pooledPlayerBullets.Add(obj);
			obj.transform.SetParent(this.transform); // set as children of Spawn Manager
		}
		
		// Enemy bullets
		for (int i = 0; i < enemyBulletPoolAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(enemyBullet);
			obj.SetActive(false);
			pooledEnemyBullets.Add(obj);
			obj.transform.SetParent(this.transform); // set as children of Spawn Manager
		}
	}

	public GameObject GetPlayerBullet()
	{
		// For as many objects as are in the pooledObjects list
		for (int i = 0; i < pooledPlayerBullets.Count; i++)
		{
			// if the pooled objects is NOT active, return that object 
			if (!pooledPlayerBullets[i].activeInHierarchy)
			{
				return pooledPlayerBullets[i];
			}
		}
		// otherwise, add to pool 
		return null;
	}
	
	public GameObject GetEnemyBullet()
	{
		// For as many objects as are in the pooledObjects list
		for (int i = 0; i < pooledEnemyBullets.Count; i++)
		{
			// if the pooled objects is NOT active, return that object 
			if (!pooledEnemyBullets[i].activeInHierarchy)
			{
				return pooledEnemyBullets[i];
			}
		}
		// otherwise, add to pool  
		GameObject obj = (GameObject)Instantiate(enemyBullet);
		obj.SetActive(false);
		pooledEnemyBullets.Add(obj);
		obj.transform.SetParent(this.transform); // set as children of Spawn Manager
		
		return obj;
	}

}
