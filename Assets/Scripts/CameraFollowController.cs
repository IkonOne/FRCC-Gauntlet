using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowController : MonoBehaviour {
    public Transform target;
    public float height;
    public float distance;

    // Update is called once per frame
    void LateUpdate () {
        if(target==null)
        {
            return;
        }
        var targetPos = target.position;
        targetPos.y += height;
        targetPos.z -= distance;

        transform.position = targetPos;
        transform.LookAt(target);
	}
}
