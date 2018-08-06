using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class MissleManager : MonoBehaviour {
    public MissleDef MissleDef;

    Pool _pool;
    List<Missle> _missles = new List<Missle>();
    List<Missle> _destroyed = new List<Missle>();

	void Start () {
        _pool = GetComponent<Pool>();
        _pool.SetPrefab(MissleDef.prefab);
	}

    public void FireMissle(Vector3 position, Vector3 direction) {
        var go = _pool.Pop(position, Quaternion.identity);
        var missle = go.GetComponent<Missle>();
        missle.transform.parent = transform;
        missle.Init();
        missle.SetManager(this);
        missle.Rigidbody.velocity = direction * MissleDef.speed;
        missle.LifeTime = 0;
        _missles.Add(missle);
    }

    public void DestroyMissle(Missle missle, bool explode = true) {
        _destroyed.Add(missle);
    }
	
	void Update () {
        // Update active missles
        foreach (var missle in _missles)
        {
            missle.LifeTime += Time.deltaTime;
            if (missle.LifeTime >= MissleDef.duration)
            {
                DestroyMissle(missle, false);
            }
        }

        // clear destroyed missles
        foreach (var missle in _destroyed)
        {
            _missles.Remove(missle);
            var go = missle.gameObject;
            _pool.Push(ref go);
        }
        if(_destroyed.Count > 0) {
            _destroyed.Clear();
        }
    }
}
