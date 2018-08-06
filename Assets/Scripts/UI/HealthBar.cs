using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour {
    public Transform PlayerTransform;
    public Image HealthBarImage;
    public float heightOverGround = 2;

    private Health _health;

	// Update is called once per frame
	void LateUpdate () {
        transform.position = _health.transform.position + Vector3.up * heightOverGround;
	}

    public void RegisterHealth(Health health) {
        health.OnHealthChanged += HandleHealthChanged;
        HandleHealthChanged(health, health.maxHealth, health.health);
        _health = health;
    }
   
    public void SetHealthRatio(float ratio) {
        ratio = Mathf.Clamp01(ratio);
        HealthBarImage.rectTransform.anchorMax = new Vector2(
            ratio,
            HealthBarImage.rectTransform.anchorMax.y
        );
    }

    private void HandleHealthChanged(Health health, float previous, float current)
    {
        var ratio = health.health / health.maxHealth;
        ratio = Mathf.Clamp01(ratio);

        // anchorMax is a struct and structs are by value in C#
        // so I am creating a copy of anchorMaxe, changing the value
        // then re-assigning with the new value
        var anchorMax = HealthBarImage.rectTransform.anchorMax;
        anchorMax.x = ratio;
        HealthBarImage.rectTransform.anchorMax = anchorMax;
    }
}