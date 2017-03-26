using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionSteal")]
public class ActionRunStealer : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        Debug.Log("Voy a correr");
        Run(controller);
    }

    private void Run(AIStateController controller)
    {
        if (!controller.target.CompareTag("RunAwayPoint"))
        {
            Debug.Log("No puedo correr");
            controller.LookForRunAwayPoint();
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        Debug.Log("Voy a ejecutar correr");
        controller.Move(controller.enemyInfo.speed * 0.5f);
    }
}
