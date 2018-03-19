using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{
    public static double health;
    public UnityEngine.UI.Slider slider;
    public static Animator anim;/*
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color (1f, 0f, 0f, 0.1f);*/
    bool damaged;
    bool isDead;

    void Start()
    {
        isDead = false;
        anim = GetComponent<Animator>();
        health = 100.0;
    }



    /*public void Update() {
        if (damaged) {

            damageImage.color = flashColor;
        } else {

            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }*/

    public void Death()
    {
        if (isDead)
            return;
        anim.SetTrigger("isDead");
        StartCoroutine("DieScreen");
        isDead = true;
    }

    public void Damage()
    {
        if (health < 0)
            return;
        health -= 0.3;
        damaged = true;
        if (health < 0)
        {
            Death();
        }
        slider.value -=0.3f;
    }

    IEnumerator DieScreen()
    {
        yield return new WaitForSeconds(3);

        Application.LoadLevel(2);
    }
}