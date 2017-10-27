using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Melee")]
public class ActionMeleeAbility : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        if(controller.objectInfo.objectMeleeAbility != null)
        {
            AbilityMelee(controller);
        }
        else return;
    }

    private void AbilityMelee(AIController controller)
    {
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        if (controller.objectMeleeTimer <= 0)
        {
            controller.objectInfo.objectMeleeAbility.Ability(controller);
            controller.SetMeleeCoolDown(controller.objectInfo.objectMeleeCooldown);
        }
        else return;
    }
}
