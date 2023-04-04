using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Navigation Executable", menuName = "GOAP/GOAP Action Executable/Navigation Executable")]
public class ExecutableFindObjectWithTag : ExecutableAction
{
    [SerializeField] LayerMask healthLayerMask;
    [SerializeField] float sensitiveRadius;
    Vector3 _target;
    Transform _childTransform;
    public override void OnExecuteBegin(GameObject Agent)
    {
        base.OnExecuteBegin(Agent);
        target = Agent;
        // Requires a class to trigger sensors on the NPC to do overlap sphere
        Collider[] _healthKits = Physics.OverlapSphere(Agent.transform.position, sensitiveRadius, healthLayerMask);
        Transform _nearestTransform = null;
        float nearest = Mathf.Infinity;
        for (int i = 0; i < _healthKits.Length; i++)
        {
            if (_healthKits[i].GetComponent<ExecutablleFindFriend>())
            {
                if(Vector3.Distance(Agent.transform.position, _healthKits[i].transform.position) < nearest)
                {
                    nearest = Vector3.Distance(Agent.transform.position, _healthKits[i].transform.position);
                    _nearestTransform = _healthKits[i].transform;
                }
            }
        }
        if(_nearestTransform != null)
        {
            _target = _nearestTransform.position;
            Agent.GetComponentInChildren<NavMeshAgent>().SetDestination(_target);
            NavMeshAgent _a = Agent.GetComponentInChildren<NavMeshAgent>();
            _childTransform = _a.transform;
        }
    }
    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if(Vector3.Distance(_childTransform.position , _target) < 1.0f)
        {
            OnExecuteEnd();
        }
    }
}
