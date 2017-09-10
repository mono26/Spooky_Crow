using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/EnemyInRange")]
public class ActionLookEnemyInRange : AIAction
{
    public override void DoAction(AIController controller)
    {
        CurrentEnemyIsInRange(controller);
    }
    private void CurrentEnemyIsInRange(AIController controller)
    {
        //Mirando la transicion.
        if (controller.objectTarget != null && controller.objectTarget.gameObject.activeInHierarchy)
        {
            var distancia = Vector3.Distance(controller.objectTarget.position, controller.transform.position);
            if (distancia < controller.objectInfo.objectRange)
            {
                return;
            }
            else if (distancia > controller.objectInfo.objectRange)
            {
                controller.ChangeTarget(null);
            }
        }
        else if (controller.objectTarget != null && !controller.objectTarget.gameObject.activeInHierarchy)
        {
            controller.ChangeTarget(null);
            return;
        }
        else return;
    }
}
