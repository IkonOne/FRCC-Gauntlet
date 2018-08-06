using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour {
    public float health=100;
    public Animator anim;
    public int dead = 0;

    // Use this for initialization
    void Start () {
        anim.SetInteger("health",(int) health);
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetInteger("health", (int)health);
        
        if (this.health <= 0f)
        {
            anim.SetTrigger("die");
           if(dead==2000)
            {
                Destroy(gameObject);
            }
            dead++;
            
            


        }
		
	}
  
        
       

    
}
