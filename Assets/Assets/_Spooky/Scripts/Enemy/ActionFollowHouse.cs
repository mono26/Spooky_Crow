using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/FollowHouse")]
public class ActionFollowHouse : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        FollowHouse(controller);
    }

    private void FollowHouse(AIController controller)
    {
        //Ejecutando movimiento a la casa!
        if (!controller.objectTarget.CompareTag("StealPoint"))
        {
            controller.objectTarget = GameManager.Instance.houseStealPoints[Random.Range(0, 5)].transform;
            return;
        }
        else
        {
            controller.objectNavMesh.SetDestination(controller.objectTarget.position);
        }
    }
}
