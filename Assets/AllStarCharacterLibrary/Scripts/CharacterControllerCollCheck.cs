using UnityEngine;
using System.Collections;

public class CharacterControllerCollCheck : MonoBehaviour 
{
	CharacterController charControl;
	float				gravity = 5.0f;



	// Use this for initialization
	void Start () 
	{
		charControl=GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//charControl.Move(Vector3.down * gravity * Time.deltaTime);
		//print (charControl.
		if(Input.GetMouseButton(0))//is the left mouse button being clicked?
		{
			//charControl.Move(new Vector3(10.0f,10.0f,10.0f));
			transform.position=new Vector3(10.0f,10.0f,10.0f);
			charControl.Move(new Vector3(0.0f,0.0f,0.0f));
		}
	}
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{

		print (hit.gameObject.name);

	}
}
