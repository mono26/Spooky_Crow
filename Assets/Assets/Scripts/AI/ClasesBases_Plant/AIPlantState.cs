using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AIComponents/PlantState")]
public class AIPlantState : ScriptableObject
{
    public AIPlantAction[] aiActions;
    public AIPlantTransition[] aiTransitions;
    public Color sceneGuizmoColor = Color.grey;

    public void UpdateState(AIPlantController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(AIPlantController controller)
    {
        for (int i = 0; i < aiActions.Length; i++)
        {
            aiActions[i].DoAction(controller);
        }
    }

    private void CheckTransitions(AIPlantController controller)
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
