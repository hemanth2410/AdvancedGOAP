using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutableAction : ScriptableObject
{

    protected GameObject target;
    [SerializeField] protected bool isSubActionRunning;

    public bool IsSubActionRunning { get { return isSubActionRunning; } }
    /// <summary>
    /// Necessary conditions to check
    /// </summary>
    public virtual void OnExecuteBegin(GameObject Agent)
    {
        isSubActionRunning = true;
        target = Agent;
    }
    /// <summary>
    /// Executng action
    /// </summary>
    public virtual void ExecuteAction()
    {
       
    }
    /// <summary>
    /// Setting states after ececuting action
    /// </summary>
    public virtual void OnExecuteEnd() 
    { 
        isSubActionRunning = false;
    }
    
}
