using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/BasicAtack")]
public class ActionBasicAtack : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        if (controller.objectInfo.objectBasicAbility != null)
        {
            AbilityBasicAtack(controller);
        }
        else return;
    }

    private void AbilityBasicAtack(AIController controller)
    {
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        if (controller.objectBasicTimer <= 0)
        {
            controller.objectInfo.objectBasicAbility.Ability(controller);
            controller.SetBasicCoolDown(controller.objectInfo.objectBasicCooldown);
        }
        else return;
    }
}
