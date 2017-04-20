using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AIAbility : ScriptableObject
{
    public abstract void Ability(GameObject obj);
}
