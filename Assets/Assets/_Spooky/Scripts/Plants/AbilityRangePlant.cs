using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Range")]
public class AbilityRangePlant : AIAbility
{
    public override void Ability(AIController controller)
    {
        RangeAtack(controller);
    }

    private void RangeAtack(AIController controller)
    {
        if (controller.GetComponent<AIPlantController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             ShootWeapons(controller);
        }
        else
            return;
    }
    void ShootWeapons(AIController controller)
    {
        var bullet = PoolsManagerBullets.Instance.GetBullet();
//        bullet.GetComponent<BulletController>().SetDirection((obj.transform.position - obj.GetComponent<AIPlantController>().my_Target.position).normalized);       //Se le da la direccion entre la planta y el target
        bullet.transform.position = controller.transform.position;
        bullet.transform.rotation = controller.transform.rotation;
    }
}
