using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
	private float rotationSpeed = 700.0f;

    // Update is called once per frame
    void Update()
    {
	    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}









