using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Delay Executable", menuName = "GOAP/GOAP Action Executable/Delay Executable")]
public class ExecutableDelay : ExecutableAction
{
    [SerializeField] float timeToWait;
    float timer;
    public override void OnExecuteBegin(GameObject Agent)
    {
        base.OnExecuteBegin(Agent);
        timer = 0;
    }
    public override void ExecuteAction()
    {
        if(isSubActionRunning)
        {
            timer += Time.deltaTime;
        }
        if(timer > timeToWait)
        {
            OnExecuteEnd();
        }
    }
    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
}
