using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
	private Vector3 startPos;
	private float repeatWidth;
	private float speed = 10;

	private void Awake()
	{
		startPos = transform.position; // Establish the default starting position 
		repeatWidth = GetComponent<BoxCollider>().size.z / 2; // Set repeat width to half of the background
	}

	private void Update()
	{
		
		transform.Translate(Vector3.back * Time.deltaTime * speed);
		
		// If background moves left by its repeat width, move it back to start position
		if (transform.position.z < startPos.z - repeatWidth)
		{
			transform.position = startPos;
		}
	}
}
