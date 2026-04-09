using UnityEngine;

public class MoveForward : MonoBehaviour
{
	private float speed = 10.0f;
	private float xBounds = 23.0f;
	private float zBounds = 11.5f;
	
    // Update is called once per frame
    void Update()
    {
	    transform.Translate(Vector3.forward * speed * Time.deltaTime);
	    
	    DisableIfOutOfBounds();
    }
    
	void DisableIfOutOfBounds()
	{
		if (transform.position.x < -xBounds || transform.position.x > xBounds || transform.position.z < -zBounds || transform.position.z > zBounds)
		{
			gameObject.SetActive(false);
		}
	   
	}
}
