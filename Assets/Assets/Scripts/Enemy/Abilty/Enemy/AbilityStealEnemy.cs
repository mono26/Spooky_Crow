using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/StealEnemy")]
public class AbilityStealEnemy : AIAbility
{
    public float cdTime;

    public override void Ability(GameObject obj)
    {
        Steal(obj);
    }

    private void Steal(GameObject obj)
    {
        var controller = obj.GetComponent<AIEnemyController>();
        controller.cdTimer2 = cdTime;
        controller.StartCoroutine(controller.Steal());

    }
}
