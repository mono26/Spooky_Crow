using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/DecisionPlant/LookShootRange")]
public class LookShootRangePlant : AIPlantDecision
{
    public override bool Decide(AIPlantController controller)
    {
        bool targetVisible = ShootRange(controller);
        return targetVisible;
    }
    private bool ShootRange(AIPlantController controller)
    {
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.my_PlantInfo.shootRange, 1 << 11);      //El segundo valor esta dentro de el enemyInfo
        //El tercer valor es para decirle al metodo en que layer buscar los colliders, es numero magico. Se puede optimizar
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Enemy"))
            {
                if (controller.my_Target != null)       //Si la planta tiene un target es porque ya esta atacando
                    return true;
                else
                {
                    Debug.Log("He encontrado a un enemigo");
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
