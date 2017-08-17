using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Melee")]
public class ActionAbility1 : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Atack(controller);
    }

    private void Atack(AIController controller)
    {
        if (controller.objectTarget != GameManager.Instance.player.transform)
        {
            controller.objectTarget = GameManager.Instance.player.transform;
            return;
        }
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        controller.CastAbility1();
    }
}
