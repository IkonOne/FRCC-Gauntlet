using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private GameObject _prefab;
    private Queue<GameObject> _queue = new Queue<GameObject>();

    public void SetPrefab(GameObject prefab) {
        Debug.Assert(prefab != null);
        _prefab = prefab;
    }

    public GameObject Pop(Vector3 position, Quaternion rotation) {
        GameObject go = null;
        if(_queue.Count > 0) {
            go = _queue.Dequeue();
            go.SetActive(true);
        }
        else {
            go = GameObject.Instantiate(_prefab, position, rotation);
        }
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.name = _prefab.name;

        return go;
    }

    public void Push(ref GameObject go) {
        go.transform.parent = transform;
        go.SetActive(false);
        _queue.Enqueue(go);
    }

    public void ReleaseAll() {
        var go = _queue.Dequeue();
        while(go != null) {
            GameObject.Destroy(go);
            go = _queue.Dequeue();
        }
    }
}
