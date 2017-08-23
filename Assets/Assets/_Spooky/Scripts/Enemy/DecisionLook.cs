using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/Look")]
public class DecisionLook : AIDecision
{
    public override bool Decide(AIController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    private bool Look(AIController controller)
    {
        if (Vector3.SqrMagnitude(controller.transform.position - controller.objectTarget.position) < (controller.objectInfo.objectRange * controller.objectInfo.objectRange) * 0.2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
