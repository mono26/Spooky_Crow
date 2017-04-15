using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionSteal")]
public class ActionRunStealer : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        Run(controller);
    }

    private void Run(AIStateController controller)
    {
        if (!controller.target.CompareTag("RunAwayPoint"))
        {
            controller.LookForRunAwayPoint();
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move(controller.enemyInfo.speed * 0.5f);
    }
}
