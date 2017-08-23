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
        if (controller.objectTarget != GameManager.Instance.playerSpooky.transform)
        {
            controller.objectTarget = GameManager.Instance.playerSpooky.transform;
            return;
        }
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        controller.CastAbility1();
    }
}
