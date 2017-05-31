using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/State")]
public class AIEnemyState : ScriptableObject
{
    public AIEnemyAction[] aiActions;
    public AIEnemyTransition[] aiTransitions;
    public Color sceneGuizmoColor = Color.grey;

    public void UpdateState(AIEnemyController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(AIEnemyController controller)
    {
        for (int i = 0; i < aiActions.Length; i++)
        {
            aiActions[i].DoAction(controller);
        }
    }

    private void CheckTransitions(AIEnemyController controller)
    {
        for (int i = 0; i < aiTransitions.Length; i++)
        {
            bool decisionState = aiTransitions[i].decision.Decide(controller);
            if(decisionState)
            {
                controller.TransitionToState(aiTransitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(aiTransitions[i].falseState);
            }
        }
    }
}
