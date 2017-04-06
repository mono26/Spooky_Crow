using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookShootRange")]
public class LookShootRange : AIDecision
{
    public override bool Decide(AIStateController controller)
    {
        bool targetVisible = ShootRange(controller);
        return targetVisible;
    }
    private bool ShootRange(AIStateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyInfo.lookRange, Color.green);

        var colliders = Physics.OverlapSphere(controller.transform.position, controller.enemyInfo.shootRange, 1 << 9);
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
