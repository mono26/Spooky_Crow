﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Range")]
public class AbilityRange : AIAbility
{
    public float cdTime;

    public override void Ability(GameObject obj)
    {
        RangeAtack(obj);
    }

    private void RangeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIPlantController>().cdTimer1 > 0)
            return;
        if (!obj.GetComponent<AIPlantController>().atacking)
        {
            obj.GetComponent<AIPlantController>().atacking = true;
            obj.GetComponent<AIPlantController>().cdTimer1 = cdTime;
            obj.GetComponent<AIPlantController>().my_MeleeCollider.enabled = true;
        }
        else
            return;
    }
    void ShootWeapons(GameObject obj)
    {
        var bullet = BulletsPool.Instance.GetBullet();
        //bullet.transform.position = obj.GetComponent<AIPlantController>().weapons[Random.Range(0, weapons.Length)].position;      //Shoot point, lugar de donde sale el proyectil
        //bullet.AddForce(obj.GetComponent<AIPlantController>().weapons[Random.Range(0, weapons.Length)].forward * shootSpeed, ForceMode.Impulse);
    }
}
