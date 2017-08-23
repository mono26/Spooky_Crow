using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/DecisionPlant/LookShootRange")]
public class LookShootRangePlant : AIDecision
{
    public override bool Decide(AIController controller)
    {
        bool targetVisible = ShootRange(controller);
        return targetVisible;
    }
    private bool ShootRange(AIController controller)
    {
        var colliders = Physics.OverlapSphere(controller.transform.position, controller.objectInfo.objectRange, 1 << 11);      //El segundo valor esta dentro de el enemyInfo
        //El tercer valor es para decirle al metodo en que layer buscar los colliders, es numero magico. Se puede optimizar
        if (colliders.Length > 0)
        {
            if(colliders[0].CompareTag("Enemy"))
            {
                if (controller.objectTarget != null)       //Si la planta tiene un target es porque ya esta atacando
                    return true;
                else
                {
                    Debug.Log("He encontrado a un enemigo");
                    controller.objectTarget = colliders[0].transform;
                    return true;
                }
            }
            controller.objectTarget = null;
            return false;
        }
        else
        {
            controller.objectTarget = null;
            return false;
        }
    }
}
