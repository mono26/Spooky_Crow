using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionSteal")]
public class ActionRunStealer : AIAction
{ 
    public override void DoAction(AIController controller)
    {
        Run(controller);
    }

    private void Run(AIController controller)
    {
        if (!controller.objectTarget.CompareTag("RunAwayPoint"))
        {
            var runIndex = Random.Range(0, GameManager.Instance.runAwayPoints.Length);
            controller.ChangeTarget(GameManager.Instance.runAwayPoints[runIndex]);
            return;
        }
    }
}
