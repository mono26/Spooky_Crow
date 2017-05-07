using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowPlayer")]
public class ActionFollowPlayer : AIEnemyAction
{ 
    public override void DoAction(AIEnemyController controller)
    {
        FollowPlayer(controller);
    }

    private void FollowPlayer(AIEnemyController controller)
    {
        if (controller.my_Target != GameManager.Instance.player.transform)
        {
            controller.my_Target = GameManager.Instance.player.transform;
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move(controller.my_EnemyInfo.speed);
    }
}
