using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/ActionsPlant/ShootPlant")]
public class ActionShootPlant : AIPlantAction
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
        controller.Ability2();       //Metodo unico que esta dentro del script de aiAtack
    }
}
