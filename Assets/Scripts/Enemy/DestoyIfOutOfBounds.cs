using UnityEngine;

public class DestoyIfOutOfBounds : MonoBehaviour
{
	private float xBounds = 23.0f;
	private float zBounds = 11.5f;

    // Update is called once per frame
    void Update()
    {
	    if (transform.position.x < -xBounds || transform.position.x > xBounds || transform.position.z < -zBounds || transform.position.z > zBounds)
	    {
		    gameObject.SetActive(false);
	    }
    }
}
