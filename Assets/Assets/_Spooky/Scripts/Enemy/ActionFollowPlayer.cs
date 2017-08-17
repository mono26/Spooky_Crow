using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowPlayer")]
public class ActionFollowPlayer : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        FollowPlayer(controller);
    }

    private void FollowPlayer(AIController controller)
    {
        if (controller.objectTarget != GameManager.Instance.player.transform)
        {
            controller.objectTarget = GameManager.Instance.player.transform;
            return;
        }
    }
}
