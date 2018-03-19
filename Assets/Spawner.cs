using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    Vector3 []vectors=new Vector3[10];
    // Update is called once per frame
    public GameObject player;
    public GameObject zombie;
    public float spawnTime = 3f;
    // Use this for initialization
    void Start () {
        vectors[0] = new Vector3(-49.914f, 0.18f, 142.27f);
        vectors[1] = new Vector3(-44.08f, 0.18f, 169.35f);
        vectors[2] = new Vector3(-44.08f, 0.18f, 211.4f);
        vectors[3] = new Vector3(2.5f, 0.18f, 246f);
        vectors[4] = new Vector3(43.3f, 0.18f, 190.2f);
        vectors[5] = new Vector3(43.3f, 0.18f, 141.44f);
        vectors[6] = new Vector3(43.3f, 0.18f, 61.2f);
        vectors[7] = new Vector3(-26.5f, 0.18f, 61.2f);
        vectors[8] = new Vector3(-105.3f, 0.18f, 61.2f);
        vectors[9] = new Vector3(-155.4f, 0.18f, 61.2f);
        InvokeRepeating("SpawnBall", spawnTime, spawnTime);
    }



    // Update is called once per frame
    void SpawnBall()
    {
        GameObject go=Instantiate(zombie, vectors[(int)(Random.value*100)%10], Quaternion.identity);
        go.GetComponent<ZombieControl>().go = player;
    }
}
