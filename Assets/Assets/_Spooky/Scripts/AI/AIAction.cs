﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public abstract void DoAction(AIController controller);
}