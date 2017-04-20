using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIPlantTransition   //A simple daata class
{
    public AIPlantDecision decision;     //Decision que se evalua para ver si se cambia de estado o no.
    public AIPlantState trueState;
    public AIPlantState falseState;
}
