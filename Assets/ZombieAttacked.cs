using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttacked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello Pignu");
        if(other.gameObject.name[0]=='z')
        {
            Debug.Log("I was called here " + other.gameObject.name);
            gameObject.GetComponent<ZombieControl>().Die();
        }
    }
}
