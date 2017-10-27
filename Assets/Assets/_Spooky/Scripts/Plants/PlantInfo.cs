using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/PlantInfo")]
[System.Serializable]
public class PlantInfo : Info {
    public enum PlantType
    {
        FISICAL, MAGIC, HARD
    }
    public PlantType plantType;
    public string plantName;
    public int plantIndex;   //Variable para almacenar la informacion de los pools.
    public int plantMeleeDamage;
    public float plantAtackSpeed;
    public float plantMeleeSpeed;
    public float plantSpecialSpeed;
    public int plantReward;

    public BulletController normalBullet;
    public BulletController specialBullet;

    public AIAbility plantMeleeAbility;
    public AIAbility plantNormalAbility;
    public AIAbility plantSpecialAbility;

    public float plantMeleeRange;
    public float plantShootRange;

    public AudioClip plantAttackClip;

    public override void InitializeInfo()
    {
        SetParentVariables();
        if(plantMeleeAbility != null)
        {
            plantMeleeAbility.InitializeAbility();
        }
        if(plantNormalAbility != null)
        {
            plantNormalAbility.InitializeAbility();
        }
        if (plantSpecialAbility != null)
        {
            plantSpecialAbility.InitializeAbility();
        }
        if(normalBullet != null)
        {
            normalBullet.Initialize();
        }
        if(specialBullet != null)
        {
            specialBullet.Initialize();
        }
        InitializeCooldowns();
    }
    private void SetParentVariables()
    {
        this.objectName = plantName;
        this.objectIndex = plantIndex;
        this.objectDamage = plantMeleeDamage;
        this.objectRange = plantShootRange;
        this.objectMeleeRange = plantMeleeRange;
        this.attackClip = plantAttackClip;
        if(plantSpecialAbility != null)
        {
            this.objectSpecialAbility = plantSpecialAbility;
        }
        if(plantNormalAbility != null)
        {
            this.objectBasicAbility = plantNormalAbility;  
        }
        if (plantMeleeAbility != null)
        {
            this.objectMeleeAbility = plantMeleeAbility;
        }
        if (normalBullet != null)
        {
            this.objectBasicBullet = normalBullet;
        }
        if (specialBullet != null)
        {
            this.objectSpecialBullet = specialBullet;
        }
    }
    public override void InitializeCooldowns()
    {
        if(plantMeleeSpeed > 0)
        {
            this.objectMeleeCooldown = plantMeleeSpeed;
        }
        if(plantAtackSpeed > 0)
        {
            this.objectBasicCooldown = plantAtackSpeed;
        }
        if(plantSpecialSpeed > 0)
        {
            this.objectSpecialCooldown = plantSpecialSpeed;
        }
    }
}
