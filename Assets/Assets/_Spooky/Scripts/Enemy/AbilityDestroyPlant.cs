using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/MeleeEnemy")]
public class AbilityDestroyPlant : AIAbility
{
    public float cooldown;

    public override void Ability(AIController controller)
    {
        MeleeAtack(controller);
    }

    private void MeleeAtack(AIController controller)
    {
        if (controller.GetComponent<AIEnemyController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
            //Debe de ir el resto del metodo de la planta para el melee
            controller.objectTarget.GetComponent<PlantPoint>().DestroyPlant();
        }
        else
            return;
    }
}
