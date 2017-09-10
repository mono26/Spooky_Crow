using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIController : MonoBehaviour
{
    [HideInInspector]
    public Info objectInfo;
    [HideInInspector]
    public float objectUpdateRate;

    [HideInInspector]
    public NavMeshAgent objectNavMesh;
    [HideInInspector]
    public Animator objectAnimator;
    [HideInInspector]
    public Transform objectSprite;

    [HideInInspector]
    public AIState objectCurrentState;
    [HideInInspector]
    public AIState objectRemainState;
    [HideInInspector]
    public AIState objectOriginalState;

    [HideInInspector]
    public Transform objectTarget;
    [HideInInspector]
    public Transform objectShootPoint;

    [HideInInspector]
    public bool objectFinishedStealing;
    [HideInInspector]
    public float objectForce;
    [HideInInspector]
    public float objectCDTimer1;
    [HideInInspector]
    public float objectCDTimer2;

    //Metodos vacios y unos obligatorio para cad uno de los controllers
    public abstract void Initialize();
    public abstract void SetCD1(float cooldown);
    public abstract void SetCD2(float cooldown);
    public abstract IEnumerator UpdateState(); 
    public virtual void ChangeTarget(Transform newTarget)
    {

    }
    public virtual void TransitionToState(AIState nextState)
    {

    }
}
