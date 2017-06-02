using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/MeleeEnemy")]
public class AbilityMeleeEnemy : AIAbility
{
    public override void Ability(GameObject obj)
    {
        MeleeAtack(obj);
    }

    private void MeleeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIEnemyController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             //Debe de ir el resto del metodo de la planta para el melee
        }
        else
            return;
    }
}
