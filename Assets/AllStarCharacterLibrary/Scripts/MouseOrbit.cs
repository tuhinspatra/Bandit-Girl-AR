using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour 
{
	public Transform target;
	float distance = 15f;
	float xSpeed = 4.0f;
	float ySpeed = 1.0f;
	float x = 15.0f;
	float y = 5.0f;
	
	
	void Start () 
	{
	    Vector3 angles = transform.eulerAngles;
    	x = angles.y;
    	y = angles.x;
		transform.rotation = Quaternion.Euler( y, x, 0.0f);
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		distance += Input.GetAxis("Mouse ScrollWheel") * 5 ;
		
		if ( Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetMouseButton(1))
			{
				distance += Input.GetAxis("Mouse Y") * 0.5f;
			}
			
			if (Input.GetMouseButton(0))
			{
				x += Input.GetAxis("Mouse X") * xSpeed*3;
				y -= Input.GetAxis("Mouse Y") * ySpeed*8;
				y = ClampAngle(y);
				x = ClampAngle(x);
				transform.rotation = Quaternion.Euler( y, x, 0.0f);
			}
			

			if (Input.GetMouseButton(2))
			{
				float x2 = Input.GetAxis("Mouse X");
				float y2 = Input.GetAxis("Mouse Y");
				target.transform.position += transform.right * (-x2*0.2f);
				target.transform.position += transform.up * (-y2 *0.2f);
			}

		}
		transform.position = target.transform.position - (transform.forward * distance);
	}
	
	float ClampAngle (float angle) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return angle;
	}
	
void OnGUI()
	{

		string tempString = "ALT+LMB to orbit,   ALT+RMB to zoom,   ALT+MMB to pan";
		GUI.Label (new Rect (10, 25,1000, 20), tempString);
	}
}
