using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/StealEnemy")]
public class AbilityStealEnemy : AIAbility
{
    public override void Ability(AIController controller)
    {
        Steal(controller);
    }

    private void Steal(AIController controller)
    {
        GameManager.Instance.LoseHealth(Random.Range(0, 50));
        Debug.Log("Termine de robar");
        controller.objectNavMesh.isStopped = false;
        controller.objectFinishedStealing = true;
    }
}
