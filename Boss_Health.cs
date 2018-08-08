using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour {
    public float health = 100;
    public Animator anim;
    public int dead = 0;

    // Use this for initialization
    void Start()
    {
        anim.SetInteger("health", (int)health);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("health", (int)health);
        if (health <= 0f)
        {
            anim.SetTrigger("Die");
            Boss.isPlayerAlive = false;

            if (dead == 2000)
            {
                Destroy(gameObject);
            }
            dead++;




        }

    }
}
