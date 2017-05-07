using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlantAction : ScriptableObject
{
    public abstract void DoAction(AIPlantController controller);
}
