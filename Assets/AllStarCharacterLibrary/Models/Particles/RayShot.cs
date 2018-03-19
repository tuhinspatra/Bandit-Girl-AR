using UnityEngine;
using System.Collections;

public class RayShot : MonoBehaviour 
{
	LineRenderer line;
	public Transform rayEnd;
	public Transform hitReport;
	public ASCLBasicController abc;
	public Enemy[]	enemies;
	public float damage;
	public float lifespan;
	float startTime;
	public Vector3 endPos;



	// Use this for initialization
	void Start () 
	{
		startTime = Time.fixedTime;
		line=GetComponent<LineRenderer>();
		line.SetPosition(0,transform.position);

		RaycastHit hit;
		Enemy nearest=null;
		float nearestDistance=10000.0f;
		Ray ray = new Ray();
		ray.direction = Vector3.Normalize(endPos-transform.position);
		ray.origin=transform.position;
		for(int i=0;i<enemies.Length;i++)
		{
			if(enemies[i].transform.GetComponent<Collider>().Raycast(ray, out hit, Vector3.Distance(transform.position, endPos)))
			{
				float d = Vector3.Distance( transform.position, enemies[i].transform.position);
				if( d<nearestDistance ) 
				{
					nearest=enemies[i];
					nearestDistance = Vector3.Distance(transform.position, enemies[i].transform.position - transform.position);
					endPos = hit.point;
					rayEnd.position=hit.point;
				}
			}
			else if(enemies[i].targeted)
			{
				float d = Vector3.Distance( transform.position, enemies[i].transform.position);
				if( d<nearestDistance ) 
				{
					nearest=enemies[i];
					nearestDistance = Vector3.Distance(transform.position, enemies[i].transform.position - transform.position);
					endPos = enemies[i].transform.position + new Vector3(0.0f,1.4f,0.0f);
					rayEnd.position = enemies[i].transform.position + new Vector3(0.0f,1.4f,0.0f);
				}
			}
		}
		if(nearest)
		{
			rayEnd.gameObject.SetActive(true);
			Transform tm = (Transform) Instantiate(hitReport , (rayEnd.position + new Vector3(0.0f,0.6f,0.0f)),Quaternion.identity);
			tm.gameObject.SetActive(true);
			Hit tmHit = tm.GetComponent<Hit>();
			tmHit.text = damage.ToString();

		}
		line.SetPosition(1,endPos);
	}
	
	// Update is called once per frame
	void Update () 
	{

		
		if((Time.fixedTime-startTime)> lifespan)
		{
			Destroy(gameObject);
		}
	}

}
