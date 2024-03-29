using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoldier_Partol : NpcGoalData
{

    public override void GetBelieves()
    {
        base.GetBelieves();
    }

    public override void evaluatePriority()
    {
        base.evaluatePriority();
        m_goalPriority = (GetComponent<NpcAgent>().Energy / 100.0f);
        modifyBelief();
    }
    public override void modifyBelief()
    {
        base.modifyBelief();
        GetComponent<NpcAgent>().InjectBelief("Energy", m_goalPriority);
    }
}
