using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Atack")]
public class ActionShootBoss : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        Atack(controller);
    }

    private void Atack(AIStateController controller)
    {
        if (controller.target != GameManager.Instance.player.transform)
        {
            controller.target = GameManager.Instance.player.transform;
            return;
        }
        controller.aiAtack.BossRangedAtack();
    }
}
