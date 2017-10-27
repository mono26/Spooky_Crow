using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/SecondaryAtack")]
public class AbilitySecondaryAtack : AIAbility
{
    public string theName;

    public override void Ability(AIController controller)
    {
        SecondaryAtack(controller);
    }

    private void SecondaryAtack(AIController controller)
    {
        if (controller.objectTarget != null)        //Si no tiene target no deberia de disparar.
        {
            ThrowSecondaryAtack(controller);
        }
        else return;
    }
    void ThrowSecondaryAtack(AIController controller)
    {
        var direction = (controller.objectTarget.position - controller.transform.position).normalized;
        var bullet = PoolsManagerBullets.Instance.GetBullet(controller.objectInfo.objectSpecialBullet.objectInfo.objectIndex);
        //bullet.GetComponent<BulletController>().my_Point = GameManager.Instance.player.transform.position;
        bullet.transform.position = controller.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(direction * controller.objectForce, ForceMode.Impulse);
    }
    public override void InitializeAbility()
    {
        abilityName = theName;
    }
}
