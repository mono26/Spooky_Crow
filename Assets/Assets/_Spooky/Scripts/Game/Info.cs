using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Info : ScriptableObject
{
    [HideInInspector]
    public string objectName;
    [HideInInspector]
    public int objectIndex;
    [HideInInspector]
    public float objectSpeed;

    [HideInInspector]
    public AIAbility ability1;
    [HideInInspector]
    public AIAbility ability2;

    [HideInInspector]
    public BulletController bullet;

    [HideInInspector]
    public float objectRange;

    public abstract void InitializeInfo();
}
