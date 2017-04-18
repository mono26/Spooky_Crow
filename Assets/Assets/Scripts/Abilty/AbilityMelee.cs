using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Melee")]
public class AbilityMeleeEnemy : AIAbility
{
    public float cdTime;

    public override void Ability(GameObject obj)
    {
        MeleeAtack(obj);
    }

    private void MeleeAtack(GameObject obj)
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
}
