using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : AIController
{
    public PlantInfo my_PlantInfo;
    public float ability1CDTimer;
    public float ability2CDTimer;

    public AIState plantCurrentState;
    public AIState plantRemainState;

    public Animator my_Animator;
    public Transform my_Target;
    public Vector3 my_LocalTarget;
    public Transform my_ShootPoint;
    public Collider my_MeleeCollider;

    private void Awake()
    {
        my_Animator = transform.GetComponent<Animator>();
    }
    private void Update()
    {
        if (ability1CDTimer > 0)
        {
            ability1CDTimer -= Time.deltaTime;
        }
        if (ability2CDTimer > 0)
        {
            ability2CDTimer -= Time.deltaTime;
        }
        plantCurrentState.UpdateState(this);

        if(my_Target != null && !my_Target.gameObject.activeInHierarchy)
        {
            my_Target = null;
        }

        Animate();
    }
    public void Animate()
    {
        if(!my_Target)
        {
            return;
        }
        if (transform.position.x <= my_Target.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (transform.position.x > my_Target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void Ability1()     //Esta es la abilidad melee, contenida en myPlantInfo
    {
        if (ability1CDTimer <= 0)
        {
            my_PlantInfo.plantAbility1.Ability(this);
        }
        else
            return;
    }
    public void Ability2()     //Esta es la abilidad ranged, contenida en myPlantInfo
    {
        if (ability2CDTimer <= 0)
        {
            ability2CDTimer = my_PlantInfo.plantAbility2.abilityCooldown;
            my_Animator.SetTrigger("Attack");
        }
        else
            return;
    }
    public override void TransitionToState(AIState nextState)
    {
        if (nextState != plantRemainState)
        {
            plantCurrentState = nextState;
        }
    }
    public override void CastAbility2()
    {
        my_PlantInfo.plantAbility2.Ability(this);
    }
    public override void CastAbility1()
    {
        my_PlantInfo.plantAbility1.Ability(this);
    }
}
