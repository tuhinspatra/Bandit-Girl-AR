using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
        text.GetComponent<Score>().ResetScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        if(other.gameObject.name=="A03")
        {
            text.GetComponent<Score>().UpScore();
            Destroy(this.gameObject);
        }
    }
}
