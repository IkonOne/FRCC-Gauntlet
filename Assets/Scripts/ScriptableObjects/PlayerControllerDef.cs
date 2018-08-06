using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "FRCC-Gauntlet/Player Controller Definition")]
public class PlayerControllerDef : ScriptableObject {
    public string HorizontalAxis;
    public string VerticalAxis;
    public string FireButton;
}
