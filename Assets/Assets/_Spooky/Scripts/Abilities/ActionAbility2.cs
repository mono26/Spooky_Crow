using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionAbility2")]
public class ActionAbility2 : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Ability2(controller);
    }

    private void Ability2(AIController controller)
    {
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        if (controller.objectInfo.objectAbility2 != null)
        {
            if (controller.objectCDTimer2 <= 0)
            {
                controller.objectInfo.objectAbility2.Ability(controller);
            }
            else return;
        }
        else return;
    }
}
