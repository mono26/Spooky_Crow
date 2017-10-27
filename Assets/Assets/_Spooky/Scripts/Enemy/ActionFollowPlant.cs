using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowPlant")]
public class ActionFollowPlant : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        FollowPlant(controller);
    }

    private void FollowPlant(AIController controller)
    {
        if (!controller.objectTarget.CompareTag("Plant"))
        {
            Debug.Log("El objeto no es planta"  + controller.objectTarget);
            return;
        }
        else
        {
            Debug.Log("Siguiendo a la planta");
            controller.objectNavMesh.isStopped = false;
            controller.objectNavMesh.SetDestination(controller.objectTarget.position);
        }
    }
}
