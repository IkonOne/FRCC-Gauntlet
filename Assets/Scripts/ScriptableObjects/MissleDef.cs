using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "FRCC-Gauntlet/Missle Def")]
public class MissleDef : ScriptableObject {
    public GameObject prefab;
    public float speed = 500;
    public float duration = 20;
}
