using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/FinishStealing")]
public class FinishStealing : AIDecision
{
    public override bool Decide(AIController controller)
    {
        bool finish = Finish(controller);
        return finish;
    }
    private bool Finish(AIController controller)
    {
        //Aqui solo vamos a mirar que el enemigo ya alla terminado de robar, ejecutar animacion, etc para poder moverlo.
        if (controller.objectFinishedStealing)
        {
            controller.objectNavMesh.isStopped = false;       //Si termino de robar sigue moviendose.
            return true;
        }
        else if (!controller.objectFinishedStealing)
        {
            controller.objectNavMesh.isStopped = true;       //Si no termino de robar sigue quieto.
            return false;
        }
        else return false;
    }
}
