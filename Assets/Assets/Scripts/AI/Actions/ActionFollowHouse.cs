using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowHouse")]
public class ActionFollowHouse : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        Debug.Log("Voy a empezar a seguir la casa");
        FollowHouse(controller);
    }

    private void FollowHouse(AIStateController controller)
    {
        if (controller.target != GameManager.Instance.house.transform)
        {
            Debug.Log("No puedo seguir la casa");
            controller.target = GameManager.Instance.house.transform;
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        Debug.Log("Estoy ejecutando el metodo");
        controller.Move(controller.enemyInfo.speed);
    }
}
