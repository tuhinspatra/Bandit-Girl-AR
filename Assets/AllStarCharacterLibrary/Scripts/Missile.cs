using UnityEngine;
using System.Collections;
#pragma warning disable
public class Missile : MonoBehaviour 
{
	public Enemy[]	enemies;
	public float speed;
	public float damage;
	public ASCLBasicController abc;
	public float lifespan;
	public float startTime;
	public Transform	hitReport;
	public Transform	particleHit;

	// Use this for initialization
	void Start () 
	{
		startTime= Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		Enemy nearest=null;
		float nearestDistance=10000.0f;
		Ray ray = new Ray();
		ray.direction=transform.forward;
		ray.origin=transform.position;
		for(int i=0;i<enemies.Length;i++)
		{
			if(enemies[i].transform.GetComponent<Collider>().Raycast(ray, out hit, speed*Time.deltaTime))
			{
				float d = Vector3.Distance( transform.position, enemies[i].transform.position);
				if( d<nearestDistance | nearestDistance==null) 
				{
					nearest=enemies[i];
					nearestDistance = Vector3.Distance(transform.position, enemies[i].transform.position - transform.position);
				}
			}
		}
		if(!nearest)
		{
			transform.position+=transform.forward*Time.deltaTime*speed;
		}
		else
		{
			Transform tm = (Transform) Instantiate(hitReport , (nearest.gameObject.transform.position + new Vector3(0.0f,1.6f,0.0f)),Quaternion.identity);
			tm.gameObject.SetActive(true);
			Hit tmHit = tm.GetComponent<Hit>();
			tmHit.text = damage.ToString();
			Transform ph = (Transform) Instantiate(particleHit , (nearest.gameObject.transform.position + new Vector3(0.0f,1.5f,0.0f)),Quaternion.identity);
			ph.transform.LookAt(Camera.main.transform.position);
			ph.transform.position += (ph.transform.forward * 2.0f);
			ph.gameObject.SetActive(true);
			Destroy(gameObject);
		}

		if((Time.fixedTime-startTime)> lifespan)
		{
			Destroy(gameObject);
		}
	}
}
