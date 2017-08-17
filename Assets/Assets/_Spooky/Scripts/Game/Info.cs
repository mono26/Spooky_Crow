using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Info : ScriptableObject
{
    public string objectName;
    public int objectIndex;
    public float objectSpeed;

    public AIAbility ability1;
    public AIAbility ability2;

    public float range;
}
