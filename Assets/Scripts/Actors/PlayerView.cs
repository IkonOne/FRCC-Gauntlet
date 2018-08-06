using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {
    public Animator Animator;

    public void SetIsIdle(bool state) {
        Animator.SetBool("IsIdle", state);
        Animator.SetBool("IsRunning", !state);
    }

    public void SetIsRunning(bool state) {
        Animator.SetBool("IsRunning", state);
        Animator.SetBool("IsIdle", !state);
    }

    public void SetIsAttacking(bool state) {
        Animator.SetBool("IsAttacking", state);
    }
}
