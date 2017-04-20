using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIEnemyDecision : ScriptableObject
{
    public abstract bool Decide(AIEnemyController controller);
}
