using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class RadialAngler : MonoBehaviour 
{
	public float angle = 360.0f;
	float oldAngle = 360.0f;
	public float range = 1.0f;
	float oldRange = 1.0f;
	public Transform diameter;
	public SkinnedMeshRenderer helper;
	// Use this for initialization
	void Start () 
	{
		diameter.localScale = new Vector3 (1.0f,1.0f,1.0f) * range *2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(oldAngle != angle)
		{
			float floatOffset = 0.5f-(angle*(0.5f/360.0f));
			Vector2 newOffset = new Vector2( 0.0f, floatOffset);
			helper.material.SetTextureOffset("_MainTex", newOffset );
			oldAngle = angle;
		}
		if(oldRange!=range)
		{
			diameter.localScale = new Vector3 (1.0f,1.0f,1.0f) * range *2;
			oldRange=range;
		}
	}
}
