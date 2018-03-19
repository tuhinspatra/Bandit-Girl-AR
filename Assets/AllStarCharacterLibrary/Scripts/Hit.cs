using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour 
{
	public TextMesh yellow;
	public TextMesh black;
	float 			startTime;
	float 			currentTime;
	public float 	lifespan; //in seconds
	
	public float 	fadeTime;	//in seconds
	public float 	fadeSpeed;	//in seconds

	public float 	maxSize;
	public float 	speed; // meters per second
	
	public string	text;
	// Use this for initialization
	void Start () 
	{
		startTime = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//destroy this hit report if we are past it's lifespan
		currentTime = Time.fixedTime - startTime;
		if(currentTime > lifespan)
		{
			Destroy(gameObject);
		}
		
		//fade out
		if(currentTime > fadeTime)
		{
			float fade = (fadeTime -(currentTime-fadeTime)) * (1/fadeSpeed);
			Color tempColor = yellow.color;
			tempColor.a = fade;
			yellow.color = tempColor;
			
			tempColor = black.color;
			tempColor.a = fade;
			black.color = tempColor;
		}
		
		//scale and lift accordingly
		if(transform.localScale.x < maxSize)
		{
			transform.localScale = new Vector3 (1.0f,1.0f,1.0f) * (0.01f + currentTime) * 3.0f;
		}
		transform.position += (Vector3.up * Time.deltaTime * speed);
		transform.LookAt(Camera.main.transform.position);
		
		yellow.text = text;
		black.text =text;
	}
}
