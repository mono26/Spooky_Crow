using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAtack : MonoBehaviour
{
    public bool atacking;

    public float meleeAtackCD;
    public float rangeAtackCD;
    [SerializeField]
    private float cdTimer1;
    [SerializeField]
    private float cdTimer2;

    public Collider clawsCollider;
    public Transform[] weapons;

    public float shootSpeed;

    //Animaciones
	
	// Update is called once per frame
	void Update ()
    {
		if(atacking)
        {
            if(cdTimer1 > 0)
            {
                cdTimer1 -= Time.deltaTime;
            }
            if (cdTimer2 > 0)
            {
                cdTimer2 -= Time.deltaTime;
            }
            else if(cdTimer1 <= 0 && cdTimer2 <= 0)
            {
                atacking = false;
                clawsCollider.enabled = false;
            }
        }
	}

    public void NormalMeleeAtack()
    {
        if (cdTimer1 > 0)
            return;
        if (!atacking)
        {
            atacking = true;
            cdTimer1 = meleeAtackCD;
            clawsCollider.enabled = true;
        }
        else
            return;
    }

    public void BossMeleeAtack()
    {
        if (cdTimer1 > 0)
            return;
        if (!atacking)
        {
            atacking = true;
            cdTimer1 = meleeAtackCD;
            clawsCollider.enabled = true;
        }
        else
            return;
    }
    
    public void BossRangedAtack()
    {
        if (cdTimer2 > 0)
            return;
        if (!atacking)
        {
            atacking = true;
            cdTimer2 = rangeAtackCD;
            ShootWeapons();
            //Metodo para que dispare las armas
        }
        else
            return;
    }

    void ShootWeapons()
    {
        var obj = BulletsPool.Instance.GetBullet();
        obj.transform.position = weapons[Random.Range(0, weapons.Length)].position;
        obj.AddForce(weapons[Random.Range(0, weapons.Length)].forward * shootSpeed, ForceMode.Impulse);
    }
}
