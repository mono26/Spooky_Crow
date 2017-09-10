using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Ability/EscapeEnemy")]
public class AbilityEscapeEnemy : AIAbility
{
    public float cooldown;
    public string theName;

    public override void InitializeAbility()
    {
        abilityCooldown = cooldown;
        abilityName = theName;
    }
    public override void Ability(AIController controller)
    {
        Escape(controller);
    }
    //Codigo Especifico de la abilidad
    private void Escape(AIController controller)
    {
        WaveSpawner.Instance.gameNumberOfEnemies--;
        controller.StopAllCoroutines();
        PoolsManagerEnemies.Instance.ReleaseObject(controller.gameObject);
    }
}
