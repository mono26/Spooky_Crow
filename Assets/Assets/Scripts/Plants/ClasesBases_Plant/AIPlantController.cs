using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : MonoBehaviour
{
    public PlantInfo my_PlantInfo;
    public float cdTimer1;
    public float cdTimer2;

    [SerializeField]
    public float cdTime1;
    [SerializeField]
    public float cdTime2;

    public AIPlantState currentState;
    public AIPlantState remainState;

    public Animator my_Animator;
    public Transform my_Target;
    public Vector3 my_LocalTarget;
    public Transform my_ShootPoint;
    public Collider my_MeleeCollider;

    private void Awake()
    {
        my_Animator = transform.GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (cdTimer1 > 0)
        {
            cdTimer1 -= Time.deltaTime;
        }
        if (cdTimer2 > 0)
        {
            cdTimer2 -= Time.deltaTime;
        }
        currentState.UpdateState(this);

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
        if (cdTimer1 <= 0)
        {
            my_PlantInfo.ability1.Ability(this.gameObject);
        }
        else
            return;
    }
    public void Ability2()     //Esta es la abilidad ranged, contenida en myPlantInfo
    {
        if (cdTimer2 <= 0)
        {
            cdTimer2 = cdTime2;
            my_Animator.SetTrigger("Attack");
        }
        else
            return;
    }
    public void TransitionToState(AIPlantState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
    private void CastAbility2()
    {
        my_PlantInfo.ability2.Ability(this.gameObject);
    }
    private void CastAbility1()
    {
        my_PlantInfo.ability1.Ability(this.gameObject);
    }
}
