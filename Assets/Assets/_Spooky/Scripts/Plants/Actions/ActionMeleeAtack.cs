using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/ActionsPlant/AtackMelee")]
public class ActionMeleeAtack : AIPlantAction
{ 
    public override void DoAction(AIPlantController controller)
    {
        Atack(controller);
    }

    private void Atack(AIPlantController controller)
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
