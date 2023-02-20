using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutableAction : ScriptableObject
{

    protected GameObject target;
    /// <summary>
    /// Necessary conditions to check
    /// </summary>
    public virtual void OnExecuteBegin()
    {

    }
    /// <summary>
    /// Executng action
    /// </summary>
    public virtual void ExecuteAction(GameObject Agent)
    {
        target = Agent;
    }
    /// <summary>
    /// Setting states after ececuting action
    /// </summary>
    public virtual void OnExecuteEnd() 
    { 

    }
}
