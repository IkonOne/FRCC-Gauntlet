using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    private int maxHealth = 20;
    private int health = 15;
    GameObject BossText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 300.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    void OnTriggerEnter(Collider other)
    {
        //If player collides with health potion and player's health is less than 20, player's health
        //increases. Player's health will only increase to a number <= 20.
        if (other.gameObject.CompareTag("healthPotion") && health < 20)
        {
            if (health + 5 > 20)
            {
                health = 20;
            }
            else
            {
                health += 5;
            }
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("BossBox"))
        {
            BossText.SetActive(true);
        }

    }
    
}
