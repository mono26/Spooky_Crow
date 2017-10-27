using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Explosion")]
public class AbilityExplosion : AIAbility
{
    public float cooldown;
    public float areaEfect;
    public float areaDamage;

    public string theName;

    public BulletController bullet;

    public GameObject explosionField;

    public override void Ability(AIController controller)
    {
        Explode(controller);
    }

    private void Explode(AIController controller)
    {
        Collider[] enemyColliders = Physics.OverlapSphere(controller.transform.position, areaEfect, 1 << 11);
        if (enemyColliders.Length > 0)
        {
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].GetComponent<AIEnemyController>().TakeDamage(areaDamage);
            }
        }
        else return;
    }

    public override void InitializeAbility()
    {
        SetParentVariables();
    }
    private void SetParentVariables()
    {
        spriteEffect = explosionField;
        abilityName = theName;
    }
}
