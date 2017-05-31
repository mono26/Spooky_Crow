using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/StealEnemy")]
public class AbilityStealEnemy : AIAbility
{
    public override void Ability(GameObject obj)
    {
        Steal(obj);
    }

    private void Steal(GameObject obj)
    {
        var controller = obj.GetComponent<AIEnemyController>();
        GameManager.Instance.LoseHealth(Random.Range(0, 50));
        Debug.Log("Termine de robar");
        controller.my_NavMeshAgent.isStopped = false;
        controller.finishStealing = true;
    }
}
