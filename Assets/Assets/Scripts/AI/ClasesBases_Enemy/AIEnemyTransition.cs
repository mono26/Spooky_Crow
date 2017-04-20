using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIEnemyTransition   //A simple daata class
{
    public AIEnemyDecision decision;     //Decision que se evalua para ver si se cambia de estado o no.
    public AIEnemyState trueState;
    public AIEnemyState falseState;
}
