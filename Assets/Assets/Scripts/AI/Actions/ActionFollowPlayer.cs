using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowPlayer")]
public class ActionFollowPlayer : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        FollowPlayer(controller);
    }

    private void FollowPlayer(AIStateController controller)
    {
        if (controller.target != GameManager.Instance.player.transform)
        {
            controller.target = GameManager.Instance.player.transform;
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move();
    }
}
