using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/BulletInfo")]
[System.Serializable]
public class BulletInfo : Info {
    public enum BulletType
    {
        FISICAL, MAGIC, HARD
    }
    public BulletType bulletType;
    public string bulletName;
    public int bulletIndex;   //Variable para almacenar la informacion de los pools.
    public int bulletDamage;

    public AIAbility bulletAbility1;

    public float bulletLifeTime;
    public float bulletSpecialLifeTime;

    public override void InitializeInfo()
    {
        if(bulletAbility1 != null)
        {
            bulletAbility1.InitializeAbility();
        }
        SetParentVariables();
        InitializeCooldowns();
    }
    private void SetParentVariables()
    {
        objectName = bulletName;
        objectIndex = bulletIndex;
        objectDamage = bulletDamage;
        objectBasicAbility = bulletAbility1;
    }
    public override void InitializeCooldowns()
    {
        if(bulletLifeTime > 0)
        {
            this.objectBasicCooldown = bulletLifeTime;
        }
        if(bulletSpecialLifeTime > 0)
        {
            this.objectSpecialCooldown = bulletSpecialLifeTime;
        }
    }
}
