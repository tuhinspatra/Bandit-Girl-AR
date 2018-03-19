using UnityEngine;
using System.Collections;

public class CamTarget : MonoBehaviour 
{
	public Transform target;
	float camSpeed = 5.0f;
	Vector3 lerpPos;

	void LateUpdate() 
	{
		//transform.position = target.position;
		lerpPos = (target.position-transform.position)* Time.deltaTime * camSpeed;
		transform.position += lerpPos;
	}
}
