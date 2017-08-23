using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/RangeEnemy")]
public class AbilityRangeEnemy : AIAbility
{
    public float cooldown;
    public string theName;

    public override void Ability(AIController controller)
    {
        RangeAtack(controller);
    }

    private void RangeAtack(AIController controller)
    {
        if (controller.objectTarget != null)        //Si no tiene target no deberia de disparar.
        {
             ShootWeapons(controller);
        }
        else
            return;
    }
    void ShootWeapons(AIController controller)
    {
        var bullet = PoolsManagerBullets.Instance.GetBullet(controller.objectInfo.objectIndex);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), controller.GetComponent<Collider>());
        //bullet.GetComponent<BulletController>().my_Point = GameManager.Instance.player.transform.position;
        bullet.transform.position = controller.transform.position;
    }
    public override void InitializeAbility()
    {
        abilityCooldown = cooldown;
        abilityName = theName;
    }
}
