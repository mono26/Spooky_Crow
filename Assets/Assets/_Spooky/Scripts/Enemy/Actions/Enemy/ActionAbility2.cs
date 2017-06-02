using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Ranged")]
public class ActionAbility2 : AIEnemyAction
{ 
    public override void DoAction(AIEnemyController controller)
    {
        Atack(controller);
    }

    private void Atack(AIEnemyController controller)
    {
        if (controller.my_Target != GameManager.Instance.player.transform)
        {
            controller.my_Target = GameManager.Instance.player.transform;
            return;
        }
        controller.Ability2();       //Metodo unico que esta dentro del script de aiAtack
    }
}
