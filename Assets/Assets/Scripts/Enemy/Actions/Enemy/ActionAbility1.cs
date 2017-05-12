using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/Melee")]
public class ActionAbility1 : AIEnemyAction
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
        controller.Ability1();
    }
}
