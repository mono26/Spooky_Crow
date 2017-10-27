using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/State")]
public class AIState : ScriptableObject
{
    public AIAction[] aiActions;
    public AITransition[] aiTransitions;

    public void UpdateState(AIController controller)
    {
        controller.StartCoroutine(DoActions(controller));
        CheckTransitions(controller);
    }

    private IEnumerator DoActions(AIController controller)
    {
        if (aiActions.Length > 0)
        {
            for (int i = 0; i < aiActions.Length; i++)
            {
                aiActions[i].DoAction(controller);
                yield return new WaitForSeconds(1 / controller.objectUpdateRate);
            }
        }
        else yield return false;
    }

    private void CheckTransitions(AIController controller)
    {
        if (aiTransitions.Length > 0)
        {
            for (int i = 0; i < aiTransitions.Length; i++)
            {
                bool decisionState = aiTransitions[i].decision.Decide(controller);
                if (decisionState)
                {
                    controller.TransitionToState(aiTransitions[i].trueState);
                    return;
                }
                else
                {
                    controller.TransitionToState(aiTransitions[i].falseState);
                }
            }
        }
        else return;
    }
}
