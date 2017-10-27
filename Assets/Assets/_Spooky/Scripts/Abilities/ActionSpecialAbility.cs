using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Special")]
public class ActionSpecialAbility : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        if (controller.objectInfo.objectSpecialAbility != null)
        {
            AbilitySpecialAtack(controller);
        }
        else return;
    }

    private void AbilitySpecialAtack(AIController controller)
    {
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        if (controller.objectSpecialTimer <= 0)
        {
            controller.objectInfo.objectSpecialAbility.Ability(controller);
            controller.SetSpecialCoolDown(controller.objectInfo.objectSpecialCooldown);
        }
        else return;
    }
}
