using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoldier_Idle : NpcGoalData
{
    public override void GetBelieves()
    {
        base.GetBelieves();
    }

    public override void evaluatePriority()
    {
        base.evaluatePriority();
        m_goalPriority = 1.0f - (GetComponent<NpcAgent>().Energy / 100.0f);
    }
}
