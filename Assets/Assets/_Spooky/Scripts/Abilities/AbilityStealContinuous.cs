using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/StealContinuous")]
public class AbilityStealContinuous : AIAbility
{
    public float cooldown;
    public string theName;

    public EnemyLoot enemyLoot;

    public override void Ability(AIController controller)
    {
        StealContinuous(controller);
    }
    private IEnumerator StealContinuous(AIController controller)
    {
        //GameManager.Instance.indicatorManager.addObject(controller.gameObject);
        Indicator3D.Instance.addObject(controller.gameObject);
        //Aqui se debe de sacar el loot del pool y ponerlo en el lootPosition
        var loot = PoolsManagerDrop.Instance.GetObject(enemyLoot.lootIndex, controller.lootPosition).GetComponent<EnemyLoot>();
        controller.GetComponent<AIEnemyController>().lootObject = loot.gameObject;
        //Para que quede en la posicion pero cuando muera no desaparezca el loot
        loot.transform.SetParent(controller.lootPosition);
        loot.SetLoot(Random.Range(0, 50));
        GameManager.Instance.LoseHealth(Random.Range(0, 25));
        while(Vector3.Distance(controller.transform.position, controller.objectTarget.position) < controller.objectInfo.objectRange)
        {
            controller.objectNavMesh.isStopped = false;
            loot.IncreaseLoot(5);
            GameManager.Instance.LoseHealth(5);
            //Con el update Rate o un steal rate adentro de la abilidad
            yield return new WaitForSeconds(1 / controller.objectUpdateRate);
        }
        controller.objectFinishedStealing = true;
        yield return null;
    }
    public override void InitializeAbility()
    {
        abilityName = theName;
    }
}
