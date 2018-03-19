using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float moveForce = 2, boostMultiplier = 2, turnSpeed;
	Rigidbody myBody;
	Animator anim;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		
		// Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));

		float vert = CrossPlatformInputManager.GetAxis ("Vertical");
		float hori = CrossPlatformInputManager.GetAxis ("Horizontal");

		bool attack = CrossPlatformInputManager.GetButtonDown ("Boost");


		anim.SetBool ("isRunning", vert > 0);
		anim.SetBool ("isBackward", vert < 0);
		if (hori > 2 || hori < 2) {
			//	print (hori);
			GetComponent<Transform> ().Rotate (0, 2*hori, 0);
		}
	}

    public void Attack()
    {
        anim.Play("2HAttack");
    }


}

