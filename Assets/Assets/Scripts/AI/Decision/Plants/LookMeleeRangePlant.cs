using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/DecisionPlant/LookMeleeRange")]
public class LookMeleeRangePlant : AIPlantDecision
{
    public override bool Decide(AIPlantController controller)
    {
        bool targetVisible = MeleeRange(controller);
        return targetVisible;
    }
    private bool MeleeRange(AIPlantController controller)
    {
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.my_PlantInfo.meleeRange, 1 << 9);      //El segundo valor esta dentro de el enemyInfo
        //El tercer valor es para decirle al metodo en que layer buscar los colliders, es numero magico. Se puede optimizar
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Crow"))
            {
                if (controller.my_Target != null)       //Si la planta tiene un target es porque ya esta atacando
                    return true;
                else
                {
                    controller.my_Target = colliders[0].transform;
                    return true;
                }
            }
            controller.my_Target = null;
            return false;
        }
        else
        {
            controller.my_Target = null;
            return false;
        }
    }
}
