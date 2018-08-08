using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour {
    GameObject BossText;
	// Use this for initialization
	void Start () {
        BossText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossBox"))
        {
            BossText.SetActive(true);
        }
    }
 }
