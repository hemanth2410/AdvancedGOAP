using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoldier_GiveCpr : NpcGoalData
{
    [SerializeField] float m_detectionRadius;
    [SerializeField] LayerMask m_detectionMask;
    public override void GetBelieves()
    {
        base.GetBelieves();
    }
    public override void evaluatePriority()
    {
        base.evaluatePriority();
        // Physics.OverLap sphere and get the colliders and check if NPC is wounded.
        // if so then inject that belief into NPC
        Vector3 _target;
        // Requires a class to trigger sensors on the NPC to do overlap sphere
        Collider[] _healthKits = Physics.OverlapSphere(transform.position, m_detectionRadius, m_detectionMask);
        Transform _nearestTransform = null;
        float nearest = Mathf.Infinity;
        for (int i = 0; i < _healthKits.Length; i++)
        {
            if (_healthKits[i].GetComponent<ExecutablleFindFriend>())
            {
                if (Vector3.Distance(transform.position, _healthKits[i].transform.position) < nearest)
                {
                    nearest = Vector3.Distance(transform.position, _healthKits[i].transform.position);
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
    }
}
