using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour {
    public MissleManager MissleManager;
    public Transform MissleSpawn;

	public void FireMissle() {
        MissleManager.FireMissle(MissleSpawn.position, transform.forward);
	}
}
