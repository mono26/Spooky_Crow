using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Decision/FinishStealing")]
public class FinishStealing : AIEnemyDecision
{
    public override bool Decide(AIEnemyController controller)
    {
        bool finish = Finish(controller);
        return finish;
    }
    private bool Finish(AIEnemyController controller)
    {
        //Aqui solo vamos a mirar que el enemigo ya alla terminado de robar, ejecutar animacion, etc para poder moverlo.
        if (controller.finishStealing)
        {
            controller.my_NavMeshAgent.isStopped = false;       //Si termino de robar sigue moviendose.
            return true;
        }
        else if (!controller.finishStealing)
        {
            controller.my_NavMeshAgent.isStopped = true;       //Si no termino de robar sigue quieto.
            return false;
        }
        else return false;
    }
}
