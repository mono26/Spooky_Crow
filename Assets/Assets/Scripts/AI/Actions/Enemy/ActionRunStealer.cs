using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionSteal")]
public class ActionRunStealer : AIEnemyAction
{ 
    public override void DoAction(AIEnemyController controller)
    {
        Run(controller);
    }

    private void Run(AIEnemyController controller)
    {
        if (!controller.my_Target.CompareTag("RunAwayPoint"))
        {
            controller.LookForRunAwayPoint();
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move(controller.enemyInfo.speed * 0.5f);
    }
}
