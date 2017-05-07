using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/MeleeEnemy")]
public class AbilityMeleeEnemy : AIAbility
{
    public float cdTime;        //Este es el cooldown de la habilidad, es especifico a la habilidad

    public override void Ability(GameObject obj)
    {
        MeleeAtack(obj);
    }

    private void MeleeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIEnemyController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             obj.GetComponent<AIEnemyController>().cdTimer1 = cdTime;
             //Debe de ir el resto del metodo de la planta para el melee
        }
        else
            return;
    }
}
