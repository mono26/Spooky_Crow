using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/ActionsPlant/ShootPlant")]
public class ActionShootPlant : AIAction
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
        controller.Ability2();       //Metodo unico que esta dentro del script de aiAtack
    }
}
