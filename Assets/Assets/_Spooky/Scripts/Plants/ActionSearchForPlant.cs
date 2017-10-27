using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/LookForPlant")]
public class ActionSearchForPlant : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        SearchForPlant(controller);
    }

    private void SearchForPlant(AIController controller)
    {
        Collider[] plantColliders = Physics.OverlapSphere(controller.transform.position, controller.objectInfo.objectRange, 1 << 13);
        if (plantColliders.Length > 0)
        {
            controller.ChangeTarget(plantColliders[0].transform);
        }
        else return;
    }
}
