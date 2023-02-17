using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Animation Executable", menuName = "GOAP/GOAP Action Executable")]
public class ExecutableAnimation : ExecutableAction
{
    //Get reference from attached gameObject
    Animator _animator;
    public override void OnExecuteBegin()
    {
        base.OnExecuteBegin();
    }

    public override void ExecuteAction()
    {
        base.ExecuteAction();
    }

    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
}
