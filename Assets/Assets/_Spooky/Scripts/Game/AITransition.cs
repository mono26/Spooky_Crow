using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITransition   //A simple daata class
{
    public AIDecision decision;     //Decision que se evalua para ver si se cambia de estado o no.
    public AIState trueState;
    public AIState falseState;
}
