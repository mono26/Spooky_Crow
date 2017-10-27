using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionRunSteal2")]
public class ActionRunStealer2 : AIAction
{
    [SerializeField]
    private float zigZagMagnitud;

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
        else
        {
            controller.objectNavMesh.isStopped = false;
            controller.objectNavMesh.SetDestination(controller.objectTarget.position);
            ZigZag(controller);
        }
    }

    private void ZigZag(AIController controller)
    {
        var zigzagPos = controller.transform.position + controller.objectNavMesh.velocity * Mathf.Sin(Time.timeSinceLevelLoad * zigZagMagnitud);
        var zigzag = Vector3.Lerp(controller.transform.position, zigzagPos, 1/controller.objectUpdateRate);
        controller.transform.position = zigzag;
    }
}
