using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour {

    public GameObject go;
    NavMeshAgent agent;
    Animator anim;

    bool isDead;
	// Use this for initialization
	void Start () {
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update () {
        if (isDead)
            return;
        if(Vector3.Distance(transform.position,go.transform.position)<3)
        {
            anim.SetBool("isNear", true);
            agent.speed=0;
        }else
        {
            anim.SetBool("isNear", false);
            agent.speed=3.5f;
        }
        agent.SetDestination(go.transform.position);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            go.GetComponent<PlayerMain>().Damage();
        }

	}

    public void Die()
    {
        if (isDead)
            return;
        anim.SetTrigger("isDead");
        isDead = true;
        Destroy(this.gameObject, 3);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Longsword")
        {
            if (go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("2HAttack"))
            {
                Die();
            }  
        }
    }
}
