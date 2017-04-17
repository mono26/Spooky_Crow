﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ShootBoss")]
public class ActionShootBoss : AIAction
{ 
    public override void DoAction(AIStateController controller)
    {
        Atack(controller);
    }

    private void Atack(AIStateController controller)
    {
        if (controller.my_Target != GameManager.Instance.player.transform)
        {
            controller.my_Target = GameManager.Instance.player.transform;
            return;
        }
        controller.aiAtack.BossRangedAtack();       //Metodo unico que esta dentro del script de aiAtack
    }
}
