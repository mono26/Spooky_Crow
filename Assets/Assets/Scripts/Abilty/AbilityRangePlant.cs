using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Range")]
public class AbilityRangePlant : AIAbility
{
    public float cdTime;

    public override void Ability(GameObject obj)
    {
        RangeAtack(obj);
    }

    private void RangeAtack(GameObject obj)
    {
        if (obj.GetComponent<AIPlantController>().my_Target != null)        //Si no tiene target no deberia de disparar.
        {
             obj.GetComponent<AIPlantController>().cdTimer2 = cdTime;
             ShootWeapons(obj);
        }
        else
            return;
    }
    void ShootWeapons(GameObject obj)
    {
        var bullet = BulletsPool.Instance.GetBullet();
        bullet.GetComponent<BulletController>().plant = true;       //Para que el bulletcontroller sepa como moverse
        bullet.GetComponent<BulletController>().player = false;
        //bullet.GetComponent<BulletController>().my_Parent = obj;      //Se le da el parent a la bala dependiento del objeto que la dispare
        bullet.GetComponent<BulletController>().my_Target = obj.GetComponent<AIPlantController>().my_Target.gameObject;        //Se le da el mismo target que el parent
        bullet.transform.position = obj.transform.position;
        bullet.transform.rotation = obj.transform.rotation;
        /*var direccion = obj.GetComponent<AIPlantController>().my_Target.position - bullet.transform.position;
        direccion.y = 0f;
        direccion = direccion.normalized;
        bullet.AddForce(direccion * 10f, ForceMode.Impulse);*/   
        //bullet.transform.position = obj.GetComponent<AIPlantController>().weapons[Random.Range(0, weapons.Length)].position;      //Shoot point, lugar de donde sale el proyectil
        //bullet.AddForce(obj.GetComponent<AIPlantController>().weapons[Random.Range(0, weapons.Length)].forward * shootSpeed, ForceMode.Impulse);
    }
}
