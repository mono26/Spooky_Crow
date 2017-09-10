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
    public int objectDamage;

    [HideInInspector]
    public float objectCooldown1;
    [HideInInspector]
    public float objectCooldown2;

    [HideInInspector]
    public GameObject objectMainSprite;
    [HideInInspector]
    public GameObject[] objectSpecialEffects;

    [HideInInspector]
    public AIAbility objectAbility1;
    [HideInInspector]
    public AIAbility objectAbility2;

    [HideInInspector]
    public BulletController objectBullet;
    [HideInInspector]
    public BulletController objectSpecialBullet;

    [HideInInspector]
    public float objectRange;

    public abstract void InitializeInfo();
}
