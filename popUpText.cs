using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpText : MonoBehaviour {
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("freelich"))
        {
            text.SetActive(true);
        }
    }

}
