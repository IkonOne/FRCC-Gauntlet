using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    Animator Chest_open_top;     //Animator class
    public GameObject lifePotPreFab;

    public GameObject spawnPotion()
    {
        return (GameObject)Instantiate(lifePotPreFab, transform.position, Quaternion.identity);
    }

    // Use this for initialization
    void Start ()
    {
        Chest_open_top = GetComponentInChildren<Animator>();
	}

    void chestCollision()
    {

        Chest_open_top.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        chestCollision();
        Invoke("destroyChest", 1);
        
    }

   void destroyChest()
    {
        spawnPotion();
        Destroy(gameObject);
    }
}
