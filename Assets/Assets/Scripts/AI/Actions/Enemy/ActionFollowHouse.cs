using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowHouse")]
public class ActionFollowHouse : AIEnemyAction
{ 
    public override void DoAction(AIEnemyController controller)
    {
        FollowHouse(controller);
    }

    private void FollowHouse(AIEnemyController controller)
    {
        if (controller.my_Target != GameManager.Instance.house.transform)
        {
            controller.my_Target = GameManager.Instance.house.transform;
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move(controller.my_EnemyInfo.speed);
    }
}
