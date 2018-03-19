using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float horizontalSpeed = 2.0F;

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
        bool isRunning = Input.GetKey("w");
        bool isBackward = Input.GetKey("s");
        bool isAttack = Input.GetButtonDown("Fire1");

        bool toAttack=false;

        if(isAttack)
        {
            toAttack = true;
        }

        anim.SetBool("isRunning", isRunning&&!isAttack);
        anim.SetBool("isBackward", isBackward&&!isAttack);
        anim.SetBool("isAttack", isAttack);

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
    }
}
