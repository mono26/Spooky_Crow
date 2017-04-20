using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : MonoBehaviour
{
    public PlantInfo my_PlantInfo;
    public float cdTimer1;
    public float cdTimer2;

    public AIPlantState currentState;
    public AIPlantState remainState;

    public Transform my_Target;
    public Transform my_ShootPoint;
    public Collider my_MeleeCollider;

    public bool atacking;

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
    }
    private void Ability1()     //Esta es la abilidad melee, contenida en myPlantInfo
    {
        my_PlantInfo.ability1.Ability(this.gameObject);
    }
    private void Ability2()     //Esta es la abilidad ranged, contenida en myPlantInfo
    {
        my_PlantInfo.ability2.Ability(this.gameObject);
    }
    public void TransitionToState(AIPlantState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
