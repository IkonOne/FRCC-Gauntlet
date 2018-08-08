using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Started by Triston R
//Made better by Erin Gunn
//Changed by Nick.s
//Add stuff to by: Luke J

public class Health : MonoBehaviour
{
    public delegate void HealthChangedDelegate(Health health, float previous, float current);
    public event HealthChangedDelegate OnHealthChanged;

    public delegate void DiedDelegate(Health health);
    public event DiedDelegate OnDied;

    public float maxHealth = 100;
    public float health = 80;
    public Animator anim;
    public float corpseTime = 20000;

    private void Start()
    {
        var hud = FindObjectOfType<HudManager>();
        hud.RegisterHealth(this);
    }

    private void OnDestroy()
    {
        var hud = FindObjectOfType<HudManager>();
        if(hud != null)
            hud.UnRegisterHealth(this);
    }

    void Update()
    {
        if(health <= 0)
        {
            DoDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Assert(damage > 0);

        var previous = health;
        health -= damage;

        if (health <= 0)
        {
            DoDeath();
        }

        OnHealthChanged(this, previous, health);
    }

    public void AddHealth(float toAdd)
    {
        Debug.Assert(toAdd > 0);

        var previous = health;
        health += toAdd;

        OnHealthChanged(this, previous, health);
    }

    void DoDeath()
    {
        Invoke("DestroyGameobject", corpseTime);
        anim.SetTrigger("die");

        // disable all colliders on the game object so that bullets and player don't hit it
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var c in colliders)
        {
            c.enabled = false;
        }

        if (OnDied != null)
        {
            OnDied(this);
        }
    }

    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}