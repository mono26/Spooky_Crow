using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [HideInInspector]
    public Info objectInfo;

    [HideInInspector]
    public NavMeshAgent objectNavMesh;
    [HideInInspector]
    public Animator objectAnimator;
    [HideInInspector]
    public Transform objectSprite;

    [HideInInspector]
    public float objectCDTimer1;
    [HideInInspector]
    public float objectCDTimer2;

    [HideInInspector]
    public AIState objectCurrentState;
    [HideInInspector]
    public AIState objectRemainState;
    [HideInInspector]
    public AIState objectOriginalState;

    [HideInInspector]
    public Transform objectTarget;

    [HideInInspector]
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
