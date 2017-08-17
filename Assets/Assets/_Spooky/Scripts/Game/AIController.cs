using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Info objectInfo;

    public AIController objectBullet;
    public NavMeshAgent objectNavMesh;

    public float objectCDTimer1;
    public float objectCDTimer2;

    public AIState objectCurrentState;
    public AIState objectRemainState;
    public AIState objectOriginalState;

    public Transform objectTarget;

    public bool objectFinishedStealing;

    public virtual void CastAbility1()
    {

    }
    public virtual void CastAbility2()
    {

    }
    public virtual void TransitionToState(AIState nextState)
    {

    }
}
