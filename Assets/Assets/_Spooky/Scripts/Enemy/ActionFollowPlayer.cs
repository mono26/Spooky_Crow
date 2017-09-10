using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowPlayer")]
public class ActionFollowPlayer : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        FollowPlayer(controller);
    }

    private void FollowPlayer(AIController controller)
    {
        //Ejecutando movimiento a la casa!
        if (!controller.objectTarget.CompareTag("Spooky"))
        {
            controller.objectTarget = GameManager.Instance.playerSpooky.transform;
            return;
        }
        else
        {
            controller.objectNavMesh.SetDestination(controller.objectTarget.position);
        }
    }
}
