using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/StealEnemy")]
public class AbilityStealEnemy : AIAbility
{
    public override void Ability(GameObject obj)
    {
        Steal(obj);
    }

    private void Steal(GameObject obj)
    {
        var controller = obj.GetComponent<AIEnemyController>();
        controller.StartCoroutine(controller.Steal());
    }
}
