using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowController : MonoBehaviour {
    /// <summary>
    /// The target this camera follows.
    /// </summary>
    public Transform target;

    /// <summary>
    /// Height off the ground of this camera.
    /// </summary>
    public float height;

    /// <summary>
    /// Distance on the ground from the target to this camera.
    /// </summary>
    public float distance;

    [Range(0.001f, 2.0f)]
    public float smoothing;

    void LateUpdate () {
        if(target == null)
            return;

        var tPos2d = new Vector2(
            target.position.x,
            target.position.z
        );

        var pos2d = new Vector2(
            transform.position.x,
            transform.position.z
        );

        var dist2d = Vector2.Distance(tPos2d, pos2d);
        var dist2dDifference = dist2d - distance;
        var normal2d = (tPos2d - pos2d).normalized;

        var heightDifference = target.position.y + height - transform.position.y;

        var translation3d = new Vector3(
            normal2d.x * dist2dDifference,
            heightDifference,
            normal2d.y * dist2dDifference
        );

        transform.position = Vector3.Lerp(transform.position, transform.position + translation3d, Time.deltaTime / smoothing);
        transform.LookAt(target);
	}
}
