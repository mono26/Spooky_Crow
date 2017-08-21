using UnityEngine;

[System.Serializable]
public abstract class AIAbility : ScriptableObject
{
    public string abilityName;      //El nombre de la habilidad solo porque si, por bonito
    public float abilityCooldown;       //El cooldown que tiene cada una de las habilidades

    public abstract void Ability(AIController controller);
}
