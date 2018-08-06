using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Pool))]
public class HudManager : MonoBehaviour {
    public GameObject HealthBarPrefab;

    private Pool _healthBarPool;
    private Canvas _canvas;
    private Dictionary<Health, HealthBar> _healthBarLookup = new Dictionary<Health, HealthBar>();

    private void Start()
    {
        _healthBarPool = GetComponent<Pool>();
        _healthBarPool.SetPrefab(HealthBarPrefab);
        _canvas = GetComponent<Canvas>();
    }

    public void RegisterHealth(Health health) {
        var go = _healthBarPool.Pop(Vector3.zero, Quaternion.identity);
        var bar = go.GetComponent<HealthBar>();
        bar.RegisterHealth(health);

        var rt = GetComponent<RectTransform>();
        var barRT = bar.GetComponent<RectTransform>();
        barRT.parent = rt;
        barRT.localScale = Vector3.one;

        _healthBarLookup[health] = bar;
    }

    public void UnRegisterHealth(Health health) {
        var bar = _healthBarLookup[health];
        var go = bar.gameObject;
        _healthBarPool.Push(ref go);
    }
}
