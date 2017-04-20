using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Atack")]
public class ActionAtack : AIEnemyAction
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
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        controller.aiAtack.NormalMeleeAtack();
    }
}
