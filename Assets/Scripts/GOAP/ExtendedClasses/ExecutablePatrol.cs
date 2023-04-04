using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Navigation Executable", menuName = "GOAP/GOAP Action Executable/Navigation Patrole Executable")]
public class ExecutablePatrol : ExecutableAction
{
    NpcAgent _agent;
    WayPointNetwork _network;
    NavMeshAgent _navMeshAgent;
    int currentIndex = 0;
    public override void OnExecuteBegin(GameObject Agent)
    {
        base.OnExecuteBegin(Agent);
        GameObject _obj = GameObject.Find("Gym Way Point Networks");
        WayPointNetwork[] _networks = _obj.GetComponentsInChildren<WayPointNetwork>();
        int _rand = Random.Range(0, _networks.Length);
        _network = _networks[_rand];
        _agent = Agent.GetComponent<NpcAgent>();
        _navMeshAgent = Agent.GetComponent<NavMeshAgent>();
    }

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        _agent.ModifyEnergy(_agent.Energy - (Time.deltaTime * 0.2f));
        if(_agent.Energy < 20.0f) { OnExecuteEnd(); }
        if(_navMeshAgent.destination == null)
        {
            _navMeshAgent.SetDestination(_network.Waypoints[0].position);
            currentIndex = 0;
        }
        if(_navMeshAgent.remainingDistance < 1.0f)
        {
            currentIndex++;
            currentIndex = currentIndex >= _network.Waypoints.Length ? 0 : currentIndex;
            _navMeshAgent.SetDestination(_network.Waypoints[currentIndex].position);
        }
    }
    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
}
