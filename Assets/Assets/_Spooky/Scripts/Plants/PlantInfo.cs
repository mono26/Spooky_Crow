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
    public PlantType my_Type;
    public string plantName;
    public int plantIndex;   //Variable para almacenar la informacion de los pools.

    public float plantAtackSpeed;

    public BulletController normalBullet;
    public BulletController specialBullet;

    public AIAbility plantAbility1;
    public AIAbility plantAbility2;

    public float meleeRange;
    public float shootRange;

    public override void InitializeInfo()
    {
        SetParentVariables();
        plantAbility1.InitializeAbility();
        if (plantAbility2 != null)
        {
            plantAbility2.InitializeAbility();
        }
        normalBullet.Initialize();
        if(specialBullet != null)
        {
            specialBullet.Initialize();
        }
    }
    private void SetParentVariables()
    {
        objectName = plantName;
        objectIndex = plantIndex;
        objectCooldown1 = plantAtackSpeed;
        if(plantAbility2 != null)
            objectCooldown2 = plantAbility2.abilityCooldown;
        objectRange = shootRange;
        objectAbility1 = plantAbility1;
        if (plantAbility2 != null)
            objectAbility2 = plantAbility2;
        objectBullet = normalBullet;
        if (specialBullet != null)
            objectSpecialBullet = specialBullet;
    }
}
