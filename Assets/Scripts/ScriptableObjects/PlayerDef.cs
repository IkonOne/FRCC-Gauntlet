using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "FRCC-Gauntlet/Player Def")]
public class PlayerDef : ScriptableObject
{
    public GameObject PlayerPrefab;
    public GameObject UIPreviewPrefab;
    public PlayerControllerDef ControllerDef;
    public MissleDef MissleDefinition;

    public float speed = 1000; // m/s

    [Range(0.0001f, float.MaxValue)]
    public float lookSmoothing = 5.0f;

    public float ReloadTime = 0.5f;
}