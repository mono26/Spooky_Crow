using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : MonoBehaviour
{
    public TowerInfo my_TowerInfo;
    public float cdTimer1;
    public float cdTimer2;

    public AIState currentState;
    public AIState remainState;

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
    }
}
