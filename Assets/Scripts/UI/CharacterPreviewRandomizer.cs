using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewRandomizer : MonoBehaviour {
    public int[] Choices;

	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetInteger("Choice", Choices[Random.Range(0, Choices.Length)]);
	}

    // Does nothing.  Here to avoid a SendMessage error
    public void FireMissle() { }
}
