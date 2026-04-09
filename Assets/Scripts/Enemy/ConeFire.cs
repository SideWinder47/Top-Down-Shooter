using UnityEngine;
using System.Collections;

public class ConeFire : MonoBehaviour
{
	[SerializeField] private AudioSource gunSound;
	 
	private float bulletSpeed = 5f;
	private float burstDelay = 2f;
	private int bulletCount = 5;
	private float coneAngle = 45f;

	private GameObject player;
	private Rigidbody playerRb;

	void Start()
	{
		player = GameObject.Find("Player");
		playerRb = player.GetComponent<Rigidbody>();

		StartCoroutine(FireCone());
	}

	void Update()
	{

		// Predictive aiming
		float distance = Vector3.Distance(transform.position, player.transform.position);
		float timeToHit = distance / bulletSpeed;

		Vector3 futurePosition = player.transform.position + playerRb.linearVelocity * timeToHit;
		Vector3 lookDirection = (futurePosition - transform.position).normalized;

		transform.rotation = Quaternion.LookRotation(lookDirection);
	}

	IEnumerator FireCone()
	{
		while (true)
		{	
			yield return new WaitForSeconds(burstDelay);
			
			float angleStep = coneAngle / (bulletCount - 1);
			float startAngle = -coneAngle / 2;
			
			for (int i = 0; i < bulletCount; i++)
			{
				// Angle of each bullet
				float currentAngle = startAngle + (angleStep * i);
				Quaternion rotation = transform.rotation * Quaternion.Euler(0, currentAngle, 0);
				
				// Spawning each bullet
				GameObject bullet = ObjectPooler.SharedInstance.GetEnemyBullet();
				bullet.transform.position = transform.position;
				bullet.transform.rotation = rotation;
				bullet.SetActive(true);
				
				// Set velocity
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				rb.linearVelocity = bullet.transform.forward * bulletSpeed;
			}
			
			gunSound.Play();
		}
	}
}