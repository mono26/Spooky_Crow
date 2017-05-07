using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/Look")]
public class DecisionLook : AIEnemyDecision
{
    public override bool Decide(AIEnemyController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    private bool Look(AIEnemyController controller)
    {
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.my_EnemyInfo.lookRange, 1 << 9);       //El segundo valor esta dentro de el enemyInfo
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
