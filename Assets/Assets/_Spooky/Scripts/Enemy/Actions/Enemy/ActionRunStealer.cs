using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/Actions/ActionSteal")]
public class ActionRunStealer : AIEnemyAction
{ 
    public override void DoAction(AIEnemyController controller)
    {
        Run(controller);
    }

    private void Run(AIEnemyController controller)
    {
        if (!controller.my_Target.CompareTag("RunAwayPoint"))
        {
            var runIndex = Random.Range(0, GameManager.Instance.runAwayPoints.Length);
            controller.my_Target = GameManager.Instance.runAwayPoints[runIndex];
            return;
        }
        //Si el target es igual al transforma de la casa el enemigo se mueve
        controller.Move(controller.my_EnemyInfo.speed * 0.5f);
    }
}
