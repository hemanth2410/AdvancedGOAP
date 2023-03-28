using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoldier_GiveCpr : NpcGoalData
{
    [SerializeField] float m_detectionRadius;
    public override void GetBelieves()
    {
        base.GetBelieves();
    }
    public override void evaluatePriority()
    {
        base.evaluatePriority();
        // Physics.OverLap sphere and get the colliders and check if NPC is wounded.
        // if so then inject that belief into NPC
    }
}
