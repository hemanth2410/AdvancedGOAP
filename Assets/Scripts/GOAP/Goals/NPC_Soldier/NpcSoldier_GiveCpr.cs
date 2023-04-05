using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcSoldier_GiveCpr : NpcGoalData
{
    [SerializeField] float m_detectionRadius;
    [SerializeField] LayerMask m_detectionMask;
    NavMeshAgent m_Agent;
    public override void GetBelieves()
    {
        base.GetBelieves();
        m_Agent = GetComponentInChildren<NavMeshAgent>();
    }
    public override void evaluatePriority()
    {
        base.evaluatePriority();
        // Physics.OverLap sphere and get the colliders and check if NPC is wounded.
        // if so then inject that belief into NPC
        Vector3 _target;
        // Requires a class to trigger sensors on the NPC to do overlap sphere
        Collider[] _healthKits = Physics.OverlapSphere(m_Agent.transform.position, m_detectionRadius, m_detectionMask);
        Transform _nearestTransform = null;
        float nearest = Mathf.Infinity;
        for (int i = 0; i < _healthKits.Length; i++)
        {
            if (_healthKits[i].GetComponent<ExecutablleFindFriend>() &&_healthKits[i].GetComponent<ExecutablleFindFriend>().NeedHelp)
            {
                if (Vector3.Distance(m_Agent.transform.position, _healthKits[i].transform.position) < nearest)
                {
                    nearest = Vector3.Distance(m_Agent.transform.position, _healthKits[i].transform.position);
                    _nearestTransform = _healthKits[i].transform;
                }
            }
        }
        if (_nearestTransform != null)
        {
            m_goalPriority = 1.0f;
        }
        else
        {
            m_goalPriority = 0.0f;
        }
        modifyBelief();
    }
    public override void modifyBelief()
    {
        base.modifyBelief();
        GetComponent<NpcAgent>().InjectBelief("BuddyDown",m_goalPriority);
    }
}
