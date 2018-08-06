using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missle : MonoBehaviour {
    public Rigidbody Rigidbody { get; set; }
    public float LifeTime { get; set; }
    public MissleManager Manager { get; private set; }
    public LayerMask groundLayer;
    public float damage = 5.0f;


	public void Init() {
        Rigidbody = GetComponent<Rigidbody>();

        // Hack to make sure the particle system starts playing as soon as it is spawned.
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Simulate(1, false, false);
        ps.Play();
	}

    public void SetManager(MissleManager manager) {
        Manager = manager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Break();
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Manager.DestroyMissle(this, false);
        }

        

        //test so see if something is in fornt of the object 
        
         
            //if the tag is the same then it will min health 
            if (collision.gameObject.tag == "Enemy")
            {
            // if (isGrounded() { }
            //  rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                Manager.DestroyMissle(this, false);


            }
        

    }
}
