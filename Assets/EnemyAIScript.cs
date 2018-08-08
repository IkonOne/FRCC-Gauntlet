using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Created by: Triston Rausch
//Edited by: Erin Gunn, Nick S.

public class EnemyAIScript : MonoBehaviour
{
    public float playerDistance;
    public float rotationDamping = 1;
    public float chaseRange = 12;
    public float moveSpeed = 3;
    public float lookRange = 1;
    public float stopDistance = 2;
    public float attackRange = 3.0f;
    public string tag;
    public Animator anim;
    public bool attacking;
    public float lag = 1.0f;
    public float attackDamage = 5.0f;

    private Transform _targetPlayer;

    // Use this for initialization
    void Start()
    {
        var health = GetComponent<Health>();
        health.OnDied += HandleDied;
    }

    // Update is called once per frame
    void Update()
    {
        // find the closest and set as player
        var players = FindObjectsOfType<PlayerController>();
        _targetPlayer = null;
        var closestDistance = Mathf.Infinity;
        foreach (var player in players)
        {
            var dist = Vector3.Distance(player.transform.position, transform.position);
            if(dist < closestDistance)
            {
                closestDistance = dist;
                _targetPlayer = player.transform;
            }
        }

        if (_targetPlayer == null)
            return;

        playerDistance = Vector3.Distance(_targetPlayer.position, transform.position);
        if (playerDistance < lookRange)
        {
            lookAtPLayer();
        }
        if (playerDistance < chaseRange)
        {
            if(!attacking)
            {
                if (playerDistance <= attackRange)
                {
                    attack();
                }
                else
                {
                    chase();
                }
            }
        }
    }

    void lookAtPLayer()
    {
        //Rotation 
        Quaternion rotation = Quaternion.LookRotation(_targetPlayer.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    void chase()
    {
        anim.SetTrigger("walk");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void attack()
    {
        RaycastHit hit;

        var layer = LayerMask.NameToLayer("Player");
        if(Physics.SphereCast(transform.position, attackRange / 2.0f, transform.forward, out hit, attackRange, 1 << LayerMask.NameToLayer("Player")))
        {

            anim.SetTrigger("attack01");
            //if the tag is the same then it will min health 
            if (hit.collider.gameObject.tag == "Player")
            {
                // if (isGrounded() { }
                //  rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            }
            attacking = true;
            StartCoroutine(attackLag());
        }
    }

    void HandleDied(Health health)
    {
        anim.SetTrigger("die");
        enabled = false;
    }

    IEnumerator attackLag()
    {
        yield return new WaitForSeconds(lag);
        attacking = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange / 2);
    //    Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange / 2);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, stopDistance);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, chaseRange);
    //}
}
