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

    public GameObject bulletSprite;
    public GameObject[] specialSprite;

    public float bulletLifeTime;
    public float specialLifeTime;

    public override void InitializeInfo()
    {
        if(bulletAbility1 != null)
        {
            bulletAbility1.InitializeAbility();
        }
        SetParentVariables();
    }
    private void SetParentVariables()
    {
        objectName = bulletName;
        objectIndex = bulletIndex;
        objectDamage = bulletDamage;
        objectAbility1 = bulletAbility1;
        objectCooldown1 = bulletLifeTime;
        objectCooldown2 = specialLifeTime;
        objectMainSprite = bulletSprite;
        objectSpecialEffects = new GameObject[specialSprite.Length];
        for(int i = 0; i < specialSprite.Length; i++)
        {
            objectSpecialEffects[i] = specialSprite[i];
        }
    }
}
