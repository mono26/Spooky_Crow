using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/ActionsPlant/AtackMelee")]
public class ActionMeleeAtack : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Atack(controller);
    }

    private void Atack(AIController controller)
    {
        /*
        if (controller.my_Target != GameManager.Instance.player.transform)
        {
            controller.my_Target = GameManager.Instance.player.transform;
            return;
        }*/
        //Aqui se debe de ejecutar el metodo que activa la habilidad
        controller.Ability1();
    }
}
