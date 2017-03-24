using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/State")]
public class AIState : ScriptableObject
{
    public AIAction[] aiActions;
    public AITransition[] aiTransitions;
    public Color sceneGuizmoColor = Color.grey;

    public void UpdateState(AIStateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(AIStateController controller)
    {
        for (int i = 0; i < aiActions.Length; i++)
        {
            aiActions[i].DoAction(controller);
        }
    }

    private void CheckTransitions(AIStateController controller)
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
