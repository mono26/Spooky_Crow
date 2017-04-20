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
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Crow"))
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
