using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAtack : MonoBehaviour
{
    public bool atacking;

    public float atackCD;
    [SerializeField]
    private float cdTimer;

    public Collider clawsCollider;

    //Animaciones
	
	// Update is called once per frame
	void Update ()
    {
		if(atacking)
        {
            if(cdTimer > 0)
            {
                cdTimer -= Time.deltaTime;
            }
            else
            {
                atacking = false;
                clawsCollider.enabled = false;
            }
        }
	}

    public void MeleeAtack()
    {
        if (!atacking)
        {
            atacking = true;
            cdTimer = atackCD;
            clawsCollider.enabled = true;
        }
        else
            return;
    }
}
