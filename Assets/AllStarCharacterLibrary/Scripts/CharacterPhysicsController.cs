
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterPhysicsController : MonoBehaviour 
{
	Animator animator;
	public GameObject		floorPlane;//in this demonstration this is set manually, the Retail Ability system has methods for dealing with this automatically via data structures for environments

	public int 				WeaponState=0;//unarmed, 1H, 2H, bow, dual, pistol, rifle, spear and ss(sword and shield)
	public bool 			wasAttacking;// we need this so we can take lock the direction we are facing during attacks, mecanim sometimes moves past the target which would flip the character around wildly

	float				rotateSpeed = 20.0f; //used to smooth out turning

	public CharacterController charcontroller;
	public Vector3 		movementTargetPosition;
	public Vector3 		attackPos;
	public Vector3		lookAtPos;
	float				gravity = 50.0f;
	public bool			jumping=false;
	public bool			grounded;
	public GameObject	contact;
	public float		verticalVelocity;
	
	RaycastHit hit;
	Ray ray;
	
	public bool rightButtonDown=false;//we use this to "skip out" of consecutive right mouse down...
	
	// Use this for initialization
	void Start () 
	{	
		animator = GetComponentInChildren<Animator>();//need this...
		charcontroller = GetComponentInChildren<CharacterController>();//need this...
		movementTargetPosition = transform.position;//initializing our movement target as our current position
	}
	
	// Update is called once per frame
	void Update () 
	{
		//The Update logic does:
		//
		//	Get UI input from keyboard, and mouse clicks
		//
		//	Tells mecanim what weaponstate we are in
		//
		//	Tells mecanim what animation we should be playing based on variables such as idling, pain or death
		//
		//	Handle movement and direction
		//
		//

		if ( ! Input.GetKey(KeyCode.LeftAlt))//if we are not using the ALT key(camera control)...
		{
			// UI goal	
			if(Input.GetMouseButton(0))//is the left mouse button being clicked?
			{
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);//get a ray that goes from the camera -> "THROUGH" the mouse pointer - > and out into the scene
				if(floorPlane.GetComponent<Collider>().Raycast(ray, out hit, 500.0f)) //check to see if that ray hits our "floor"												
				{
					movementTargetPosition = hit.point;//mark it where it hit
					wasAttacking = false;//we're moving now, not attacking
				}
			}
		}



		switch(Input.inputString)//get keyboard input, probably not a good idea to use strings here...Garbage collection problems with regards to local string usage are known to happen
		{						 //the garbage collection memory problem arises from local alloction of memory, and not freeing it up efficiently
			case "0":
				WeaponState = 0;//unarmed
				break;
			case "1":
				WeaponState = 1;//1H one handed weapon
				break;
			case "2":
				WeaponState = 2;//2H two handed weapon(longsword or heavy axe)
				break;
			case "3":
				WeaponState = 3;//bow
				break;
			case "4":
				WeaponState = 4;//dual weild(short swords, light axes)
				break;
			case "5":
				WeaponState = 5;//pistol
				break;
			case "6":
				WeaponState = 6;//rifle
				break;
			case "7":
				WeaponState = 7;//spear
				break;
			case "8":
				WeaponState = 8;//Sword and Shield
				break;
			
			case "p":
				animator.SetTrigger("Pain");//the animator controller will detect the trigger pain and play the pain animation
				break;
			case "a":
				animator.SetInteger("Death", 1);//the animator controller will detect death=1 and play DeathA
				break;
			case "b":
				animator.SetInteger("Death", 2);//the animator controller will detect death=2 and play DeathB
				break;
			case "c":
				animator.SetInteger("Death", 3);//the animator controller will detect death=3 and play DeathC
				break;
			case "n":
				animator.SetBool("NonCombat", true);//the animator controller will detect this non combat bool, and go into a non combat state "in" this weaponstate
				break;
			default:
				break;
		}
		
		animator.SetInteger("WeaponState", WeaponState);// probably would be better to check for change rather than bashing the value in like this
		
		if ( ! Input.GetKey(KeyCode.LeftAlt)) // if we're changing camera transforms, do not use "USE"
		{
			if(Input.GetMouseButton(1))// are we using the right button?
			{
				if(rightButtonDown != true)// was it previously down? if so we are already using "USE" bailout (we don't want to repeat attacks 800 times a second...just once per press please
				{// RUNGY addin enemy collider "test for nearest hit" here, they need to actually take precednce over the ground
					
					ray = Camera.main.ScreenPointToRay (Input.mousePosition);// make a ray based on the camera and mouse pointer
					if(floorPlane.GetComponent<Collider>().Raycast(ray, out hit, 500.0f)) 												
					{
						movementTargetPosition = transform.position; //we are attacking so lock our position to where we are
						attackPos = hit.point;// establish the point that we hit with the mouse
						attackPos.y = transform.position.y;//use our height for the LOOKAT function, so we stay level and dont lean the character in weird angles
						Vector3 attackDelta = attackPos - transform.position;//we need the Vector delta which is an un-normalized direction vector
						attackPos = transform.position + attackDelta.normalized * 20.0f;//look 20 meters ahead, so we don't spin around wildly if mecanim moves past the target
						animator.SetTrigger("Use");//tell mecanim to do the attack animation(trigger)
						animator.SetBool("Idling", true);//stop moving
						rightButtonDown = true;//right button was not down before, mark it as down so we don't attack 800 frames a second 
						wasAttacking =true;//some mecanims will actually move us past the target, so we want to keep looking in one direction instead of spinning wildly around the target
					}
				}
			}
		}
		
		if (Input.GetMouseButtonUp(1))//ok, we can clear the right mouse button and use it for the next attack
		{
			if (rightButtonDown == true)
			{
				rightButtonDown = false;
			}
		}
		
		Debug.DrawLine ((movementTargetPosition + transform.up*2), movementTargetPosition);//useful for visuals in editor
				
		//AttackCode has to go here for targeting reasons
		Vector3 deltaTarget = movementTargetPosition - transform.position;
		if(!wasAttacking)
		{
			lookAtPos = transform.position + deltaTarget.normalized*2.0f;
			lookAtPos.y = transform.position.y;
		}
		else
		{
			lookAtPos = attackPos;
		}
		movementTargetPosition.y=transform.position.y;
		Quaternion tempRot = transform.rotation; 	//save current rotation
		transform.LookAt(lookAtPos);						
		Quaternion hitRot = transform.rotation;		// store the new rotation
		// now we slerp orientation
		transform.rotation = Quaternion.Slerp(tempRot, hitRot, Time.deltaTime * rotateSpeed);
		
		if(Vector3.Distance(movementTargetPosition,transform.position)>0.5f)
		{
			animator.SetBool("Idling", false);
		}
		else
		{
			animator.SetBool("Idling", true);
		}

		if ( Input.GetKey(KeyCode.Space) && !jumping)//if we are not using the ALT key(camera control)...
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);// make a ray based on the camera and mouse pointer
			if(floorPlane.GetComponent<Collider>().Raycast(ray, out hit, 500.0f)) 												
			{
				verticalVelocity = 3.0f * (Vector3.Distance(transform.position,hit.point)+1.0f);
				movementTargetPosition = hit.point;//mark it where it hit
				wasAttacking = false;//we're moving now, not attacking
			}
			else
			{
				verticalVelocity = 10.0f;
			}
			jumping=true;
			verticalVelocity = 3.0f * (Vector3.Distance(transform.position,movementTargetPosition)+1.0f);
			charcontroller.Move(new Vector3(0.0f,verticalVelocity*Time.deltaTime,0.0f));
			//charcontroller.attachedRigidbody.AddForce(new Vector3(0.0f,5.0f,0.0f));
		}


		if(charcontroller.isGrounded)
		{
			grounded=true;
			verticalVelocity=-0.5f*Time.deltaTime;
			jumping=false;
		}
		else
		{
			grounded=false;
		}
		verticalVelocity-= gravity * Time.deltaTime;
		charcontroller.Move(new Vector3(0.0f,verticalVelocity*Time.deltaTime,0.0f));
		//charcontroller.SimpleMove(new Vector3(0.0f,verticalVelocity*Time.deltaTime,0.0f));
	}
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		
		contact=hit.gameObject;
		
	}
	void OnGUI()
	{
		string tempString = "LMB=move RMB=attack p=pain abc=deaths 12345678 0=change weapons";
		GUI.Label (new Rect (10, 5,1000, 20), tempString);
	}
}
