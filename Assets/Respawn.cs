using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public Vector3 center;
    public Vector3 size;
    public GameObject Enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Q))
        {
            spawnEnemy();
        }
		
	}
    public void spawnEnemy()
    {
        Vector3 pos = center + new Vector3(Random.RandomRange(-size.x / 2, size.x / 2), Random.RandomRange(-size.y / 2, size.y / 2), Random.RandomRange(-size.z / 2, size.z / 2));
        Instantiate(Enemy, pos, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);

    }
}
