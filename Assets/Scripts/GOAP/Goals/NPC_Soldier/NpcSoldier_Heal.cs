using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoldier_Heal : NpcGoalData
{
    [SerializeField] float health;
    public override void GetBelieves()
    {
        base.GetBelieves();
    }
    public override void evaluatePriority()
    {
        base.evaluatePriority();
        _believes.TryGetValue("Health", out health);
        health = health / 100.0f;
        m_goalPriority = 1 - health; // this 0.5f should be health
        modifyBelief();
    }
    public override void modifyBelief()
    {
        base.modifyBelief();
        GetComponent<NpcAgent>().InjectBelief("LowHealth", m_goalPriority);
    }
}
