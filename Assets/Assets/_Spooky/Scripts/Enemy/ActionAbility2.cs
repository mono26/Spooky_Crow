using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Ranged")]
public class ActionAbility2 : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Atack(controller);
    }

    private void Atack(AIController controller)
    {
        if (controller.objectTarget != GameManager.Instance.playerSpooky.transform)
        {
            controller.objectTarget = GameManager.Instance.playerSpooky.transform;
            return;
        }
        controller.CastAbility2();       //Metodo unico que esta dentro del script de aiAtack
    }
}
