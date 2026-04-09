using UnityEngine;
using System.Collections;

public class SustainedFire : MonoBehaviour
{
	[SerializeField] private AudioSource gunSound;

	private float bulletDelay = 0.3f;
	private float burstDelay = 1f;
	private float bulletSpeed = 10f;

	private GameObject player;
	private Rigidbody playerRb;

	void Start()
	{
		player = GameObject.Find("Player");
		playerRb = player.GetComponent<Rigidbody>();

		StartCoroutine(SpawnBullet());
	}

	void Update()
	{

		// Distance to player
		float distance = Vector3.Distance(transform.position, player.transform.position);

		// Time it takes bullet to reach player
		float timeToHit = distance / bulletSpeed;

		// Predict future position
		Vector3 futurePosition = player.transform.position + playerRb.linearVelocity * timeToHit;

		Vector3 lookDirection = (futurePosition - transform.position).normalized;

		transform.rotation = Quaternion.LookRotation(lookDirection);
	}

	IEnumerator SpawnBullet()
	{
		while (true)
		{
			yield return new WaitForSeconds(burstDelay);
			
			for (int i = 0; i < 3; i++)
			{
				// Spawning each bullet
				GameObject bullet = ObjectPooler.SharedInstance.GetEnemyBullet();
				bullet.transform.position = transform.position;
				bullet.transform.rotation = transform.rotation;
				bullet.SetActive(true);
				gunSound.Play();

				// Apply velocity to bullet
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				rb.linearVelocity = transform.forward * bulletSpeed;

				yield return new WaitForSeconds(bulletDelay);
			}
		}
	}
}