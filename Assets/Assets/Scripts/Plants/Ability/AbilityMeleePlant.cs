using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Melee")]
public class AbilityMeleePlant : AIAbility
{
    public float cdTime;        //Este es el cooldown de la habilidad, es especifico a la habilidad

    public override void Ability(GameObject obj)
    {
        MeleeAtack(obj);
    }

    private void MeleeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIPlantController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             obj.GetComponent<AIPlantController>().cdTimer1 = cdTime;
             //Debe de ir el resto del metodo de la planta para el melee
        }
        else
            return;
    }
}
