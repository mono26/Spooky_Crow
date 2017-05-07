﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookMeleeRange")]
public class LookMeleeRange : AIEnemyDecision
{
    public override bool Decide(AIEnemyController controller)
    {
        bool targetVisible = MeleeRange(controller);
        return targetVisible;
    }
    private bool MeleeRange(AIEnemyController controller)
    {
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.my_EnemyInfo.meleeRange, 1 << 9);      //El segundo valor esta dentro de el enemyInfo
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Crow"))
            {
                controller.my_NavMeshAgent.isStopped = true;        //Si el encuentra al cuervo en su rango de disparo para y dispara.
                return true;
            }
            controller.my_NavMeshAgent.isStopped = false;       //Si no lo encuentra sigue moviendose.
            return false;
        }
        else
        {
            controller.my_NavMeshAgent.isStopped = false;       //Si no lo encuentra sigue moviendose.
            return false;
        }
    }
}