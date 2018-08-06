using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour {
    public float bonusHealth = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<Health>();
            playerHealth.AddHealth(bonusHealth);
            Destroy(gameObject);
        }
    }
}
