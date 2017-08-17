using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public AIAction[] aiActions;
    public AITransition[] aiTransitions;

    public void UpdateState(AIController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(AIController controller)
    {
        for (int i = 0; i < aiActions.Length; i++)
        {
            aiActions[i].DoAction(controller);
        }
    }

    private void CheckTransitions(AIController controller)
    {
        for (int i = 0; i < aiTransitions.Length; i++)
        {
            bool decisionState = aiTransitions[i].decision.Decide(controller);
            if (decisionState)
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
