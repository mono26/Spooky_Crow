using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookMeleeRange")]
public class LookMeleeRange : AIDecision
{
    public override bool Decide(AIController controller)
    {
        bool targetVisible = MeleeRange(controller);
        return targetVisible;
    }
    private bool MeleeRange(AIController controller)
    {
        if (controller.objectCDTimer1 <= 0)
        {
            var colliders = Physics.OverlapSphere(controller.transform.position, controller.objectInfo.range, 1 << 9);      //El segundo valor esta dentro de el enemyInfo
            if (colliders.Length > 0)
            {
                if (colliders[0].CompareTag("Crow"))
                {
                    controller.objectNavMesh.isStopped = true;        //Si el encuentra al cuervo en su rango de disparo para y dispara.
                    return true;
                }
                controller.objectNavMesh.isStopped = false;       //Si no lo encuentra sigue moviendose.
                return false;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }
}
