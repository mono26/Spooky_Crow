using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/RangeEnemy")]
public class AbilityRangeEnemy : AIAbility
{
    public override void Ability(GameObject obj)
    {
        RangeAtack(obj);
    }

    private void RangeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIEnemyController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             ShootWeapons(obj);
        }
        else
            return;
    }
    void ShootWeapons(GameObject obj)
    {
        var bullet = BulletsPool.Instance.GetBullet();
        bullet.GetComponent<BulletController>().plant = false;       //Para que el bulletcontroller sepa como moverse
        bullet.GetComponent<BulletController>().player = true;
        bullet.GetComponent<BulletController>().my_Point = GameManager.Instance.player.transform.position;
        bullet.transform.position = obj.transform.position;
    }
}
