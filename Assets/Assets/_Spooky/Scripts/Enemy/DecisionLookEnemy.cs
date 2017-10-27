using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookEnemy")]
public class DecisionLookEnemy : AIDecision
{
    public override bool Decide(AIController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    private bool Look(AIController controller)
    {
        //Mirando la transicion.
        if (controller.objectTarget != null && controller.objectTarget.CompareTag("Enemy"))
        {
            var distancia = Vector3.Distance(controller.objectTarget.position, controller.transform.position);
            if (distancia <= controller.objectInfo.objectRange)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
}
