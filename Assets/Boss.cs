using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public Transform player;
    public float playerDistance;
    public float rotationDamping = 1;
    public float chaseRage = 20;
    public float moveSpeed = 3;
    public float lookRange = 30;
    public float stopDistance = 10;
    public float missChage = 50;
    int number = 0;
    public static bool isPlayerAlive = true;
    public string tags;
    public Animator anim;
    /*
    private Rigidbody rb;
    public LayerMask groundLayers;
    public float jumpforce = 7;
    public CapsuleCollider col;

    */

    // Use this for initialization
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // col = GetComponent<CapsuleCollider>();
        // anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive)
        {
            playerDistance = Vector3.Distance(player.position, transform.position);
            if (playerDistance < lookRange)
            {
                lookAtPLayer();
            }
            if (playerDistance < chaseRage)
            {
                if (playerDistance > stopDistance)
                {
                    chase();
                }
                else if (playerDistance < 2f)
                {

                    // number=  Random.Range(1, 100);


                    attack();

                }
            }
        }



    }
    void lookAtPLayer()
    {
        //Rotation 
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);

    }
    void chase()
    {
        float test = this.gameObject.GetComponent<healthScript>().health;
        //anim.Play("Walk");


        anim.SetTrigger("Walk");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }
    void attack()
    {
        RaycastHit hit;

        //test so see if something is in fornt of the object 
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            anim.SetTrigger("Attack01");
            //if the tag is the same then it will min health 
            if (hit.collider.gameObject.tag == tags)
            {
                // if (isGrounded() { }
                //  rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<healthScript>().health -= 5f;



            }
        }
    }
}
