using UnityEngine;
using System.Collections;

// using system.serializable so unity can show the values
// in the inspector and can be edited
// removing the Mono Behaviour class because
// we don't want ot attach this script to any
// GameObject
[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;
}
