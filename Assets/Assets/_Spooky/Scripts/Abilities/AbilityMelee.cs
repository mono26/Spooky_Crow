using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/MeleeAtack")]
public class AbilityMelee : AIAbility
{
    public float cooldown;
    public string theName;

    public override void Ability(AIController controller)
    {
        controller.StartCoroutine(MeleeAtack(controller));
    }
    private IEnumerator MeleeAtack(AIController controller)
    {
        if (controller.objectTarget != null)        //Si no tiene target no deberia de disparar.
        {
            controller.objectMeleeCollider.gameObject.SetActive(true);
            Vector3 direccion = controller.objectTarget.position - controller.transform.position;
            direccion.y = 0f;
            direccion = direccion.normalized;
            controller.objectMeleeCollider.transform.position = controller.transform.position + (direccion * controller.objectInfo.objectMeleeRange);
            //Debe de ir el resto del metodo de la planta para el melee
            //DDeberia activar la ejecucion de la aniacion y esperar a que pase pare activar.
            
            yield return new WaitForSeconds(0.25f);
            controller.objectMeleeCollider.gameObject.SetActive(false);
        }
        else yield return false;
    }
    public override void InitializeAbility()
    {
        abilityName = theName;
    }
}
