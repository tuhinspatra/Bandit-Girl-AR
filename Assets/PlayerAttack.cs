using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject go;

    public void attack()
    {
        go.GetComponent<PlayerController>().Attack();
    }
}
