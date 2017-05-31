using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIEnemyAction : ScriptableObject
{
    public abstract void DoAction(AIEnemyController controller);
}
