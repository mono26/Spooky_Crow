﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookMeleeRange")]
public class LookMeleeRange : AIDecision
{
    public override bool Decide(AIStateController controller)
    {
        bool targetVisible = MeleeRange(controller);
        return targetVisible;
    }
    private bool MeleeRange(AIStateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyInfo.lookRange, Color.green);

        var colliders = Physics.OverlapSphere(controller.transform.position, controller.enemyInfo.meleeRange, 1 << 9);      //El segundo valor esta dentro de el enemyInfo
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Crow"))
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
