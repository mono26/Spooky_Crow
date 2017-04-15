using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/LookStealRange")]
public class LookStealRange : AIDecision
{
    public override bool Decide(AIStateController controller)
    {
        bool targetVisible = StealRange(controller);
        return targetVisible;
    }
    private bool StealRange(AIStateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyInfo.lookRange, Color.green);

        var colliders = Physics.OverlapSphere(controller.transform.position, 1.0f, 1 << 10);        //El segundo valor de entrada es unico. No pertenece a ninguna clase y debe de ser modificado aqui.
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("House"))
            {
                controller.StartCoroutine(controller.StartStealAndRun());   //Si encunetra la casa dentro de su rango de robar empieza el proceso de robar: animacion, metodos, etc.
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
