using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/RangeEnemy")]
public class AbilityRangeBasicAtack : AIAbility
{
    public string theName;

    public override void Ability(AIController controller)
    {
        Debug.Log("Ejecutando Basic Attack");
        BasicAtack(controller);
        controller.SetCD1(controller.objectInfo.objectCooldown1);
    }

    private void BasicAtack(AIController controller)
    {
        if (controller.objectTarget != null)        //Si no tiene target no deberia de disparar.
        {
             ThrowBasicAtack(controller);
        }
        else return;
    }
    void ThrowBasicAtack(AIController controller)
    {
        var direction = (controller.objectTarget.position - controller.transform.position).normalized;
        var bullet = PoolsManagerBullets.Instance.GetBullet(controller.objectInfo.objectBullet.objectInfo.objectIndex);
        //bullet.GetComponent<BulletController>().my_Point = GameManager.Instance.player.transform.position;
        bullet.transform.position = controller.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(direction * controller.objectForce, ForceMode.Impulse);
    }
    public override void InitializeAbility()
    {
        abilityName = theName;
    }
}
