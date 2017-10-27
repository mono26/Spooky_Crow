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
    public float objectMovementSpeed;
    [HideInInspector]
    public int objectDamage;

    [HideInInspector]
    public float objectMeleeCooldown;
    [HideInInspector]
    public float objectBasicCooldown;
    [HideInInspector]
    public float objectSpecialCooldown;

    [HideInInspector]
    public AIAbility objectMeleeAbility;
    [HideInInspector]
    public AIAbility objectBasicAbility;
    [HideInInspector]
    public AIAbility objectSpecialAbility;

    [HideInInspector]
    public BulletController objectBasicBullet;
    [HideInInspector]
    public BulletController objectSpecialBullet;

    [HideInInspector]
    public float objectRange;
    [HideInInspector]
    public float objectMeleeRange;

    [HideInInspector]
    public AudioClip attackClip;

    public abstract void InitializeInfo();
    public abstract void InitializeCooldowns();
}
