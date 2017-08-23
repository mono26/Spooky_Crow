﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/MeleeEnemy")]
public class AbilityMeleeEnemy : AIAbility
{
    public override void Ability(AIController controller)
    {
        MeleeAtack(controller);
    }

    private void MeleeAtack(AIController controller)
    {
        if (controller.objectTarget != null)        //Si no tiene target no deberia de disparar.
        {
            //Debe de ir el resto del metodo de la planta para el melee
            FightCloud.Instance.SetFight(GameManager.Instance.playerSpooky, controller.gameObject);
        }
        else return;
    }
    public override void InitializeAbility()
    {
        throw new NotImplementedException();
    }
}
