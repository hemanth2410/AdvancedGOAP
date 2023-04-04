using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Idle Energy Executable", menuName = "GOAP/GOAP Action Executable/Idle Energy Executable")]
public class ExecutableEnergyReGen : ExecutableAction
{
    NpcAgent _agent;
    public override void OnExecuteBegin(GameObject Agent)
    {
        base.OnExecuteBegin(Agent);
        _agent = Agent.GetComponent<NpcAgent>();
    }
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        _agent.ModifyEnergy(_agent.Energy + (Time.deltaTime * 0.5f));
        if(_agent.Energy > 90.0f ) { OnExecuteEnd(); }
    }
    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
}
