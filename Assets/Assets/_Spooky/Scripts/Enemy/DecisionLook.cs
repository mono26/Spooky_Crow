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
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.objectInfo.range, 1 << 9);       //El segundo valor esta dentro de el enemyInfo
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
