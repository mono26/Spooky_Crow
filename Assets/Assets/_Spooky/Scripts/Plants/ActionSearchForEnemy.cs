using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/LookForEnemy")]
public class ActionSearchForEnemy : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        SearchForEnemy(controller);
    }

    private void SearchForEnemy(AIController controller)
    {
        Collider[] enemyColliders = Physics.OverlapSphere(controller.transform.position, controller.objectInfo.objectRange, 1 << 11);
        if (enemyColliders.Length > 0)
        {
            controller.ChangeTarget(enemyColliders[0].transform);
        }
        else return;
    }
}
