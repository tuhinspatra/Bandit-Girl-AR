using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class EagleView : MonoBehaviour {

    // Use this for initialization
    public Camera camera1;
    public Camera camera2;
    void Start () {
        camera1.enabled = false;
        camera2.enabled = true;
	}
	
    public void switchCam()
    {
        VuforiaRuntime.Instance.InitVuforia();
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
