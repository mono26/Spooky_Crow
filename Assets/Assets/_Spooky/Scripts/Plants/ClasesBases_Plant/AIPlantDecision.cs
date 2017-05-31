using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlantDecision : ScriptableObject
{
    public abstract bool Decide(AIPlantController controller);
}
