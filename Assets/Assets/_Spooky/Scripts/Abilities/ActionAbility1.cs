using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Melee")]
public class ActionAbility1 : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Ability1(controller);
    }

    private void Ability1(AIController controller)
    {
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        if (controller.objectCDTimer1 <= 0)
        {
            controller.objectInfo.objectAbility1.Ability(controller);
        }
        else return;
    }
}
