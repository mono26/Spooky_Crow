using UnityEngine;

[System.Serializable]
public abstract class AIAbility : ScriptableObject
{
    [HideInInspector]
    public string abilityName;      //El nombre de la habilidad solo porque si, por bonito
    [HideInInspector]
    public float abilityCooldown;       //El cooldown que tiene cada una de las habilidades

    public abstract void Ability(AIController controller);
    public abstract void InitializeAbility();
}
