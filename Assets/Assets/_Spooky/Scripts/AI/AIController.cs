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
    public Collider objectMeleeCollider;

    [HideInInspector]
    public AIState objectCurrentState;
    [HideInInspector]
    public AIState objectRemainState;
    [HideInInspector]
    public AIState objectOriginalState;

    //[HideInInspector]
    public Transform objectTarget;
    [HideInInspector]
    public Transform objectShootPoint;

    [HideInInspector]
    public bool objectFinishedStealing;
    [HideInInspector]
    public float objectForce;
    [HideInInspector]
    public float objectMeleeTimer;
    [HideInInspector]
    public float objectBasicTimer;
    [HideInInspector]
    public float objectSpecialTimer;

    [HideInInspector]
    public SoundPlayer soundPlayer;
    [HideInInspector]
    public Transform lootPosition;

    //Metodos vacios y unos obligatorio para cad uno de los controllers
    public abstract void Initialize();
    public virtual void SetMeleeCoolDown(float cooldown)
    {

    }
    public abstract void SetBasicCoolDown(float cooldown);
    public abstract void SetSpecialCoolDown(float cooldown);
    public abstract IEnumerator UpdateState(); 
    public virtual void ChangeTarget(Transform newTarget)
    {

    }
    public virtual void TransitionToState(AIState nextState)
    {

    }
}
