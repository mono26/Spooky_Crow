using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/IsPlayerInFight")]
public class ActionLookPlayerInFight : AIAction
{
    public override void DoAction(AIController controller)
    {
        IsPlayerInFight(controller);
    }
    private void IsPlayerInFight(AIController controller)
    {
        //Mirando la transicion.
        if (controller.objectTarget != null && !FightCloud.Instance.isFighting)
        {
            controller.objectNavMesh.isStopped = false;
        }
        else if (controller.objectTarget != null && FightCloud.Instance.isFighting)
        {
            controller.objectNavMesh.isStopped = true;
        }
        else return;
    }
}
