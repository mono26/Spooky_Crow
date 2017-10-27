using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/Steal")]
public class AbilitySteal : AIAbility
{
    public float cooldown;
    public string theName;

    public EnemyLoot enemyLoot;

    public override void Ability(AIController controller)
    {
        Steal(controller);
    }
    private void Steal(AIController controller)
    {
        //GameManager.Instance.indicatorManager.addObject(controller.gameObject);
        Indicator3D.Instance.addObject(controller.gameObject);
        //Aqui se debe de sacar el loot del pool y ponerlo en el lootPosition
        var loot = PoolsManagerDrop.Instance.GetObject(enemyLoot.lootIndex, controller.lootPosition).GetComponent<EnemyLoot>();
        controller.GetComponent<AIEnemyController>().lootObject = loot.gameObject;
        //Para que quede en la posicion pero cuando muera no desaparezca el loot
        loot.transform.SetParent(controller.lootPosition);
        loot.SetLoot(Random.Range(0, 50));
        GameManager.Instance.LoseHealth(Random.Range(0, 50));
        controller.objectNavMesh.isStopped = false;
        controller.objectFinishedStealing = true;
    }
    public override void InitializeAbility()
    {
        abilityName = theName;
    }
}
