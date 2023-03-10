using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] List<NpcGoalData> m_NpcGoals;
    NpcGoalData _NpcGoalData;
    public List<NpcGoalData> NpcGoals { get { return m_NpcGoals; } }
    public NpcGoalData NpcGoalData { get { return _NpcGoalData; } }
    //This class should be able to dynamically adjust the goal on priority
    //and needs to make sure that only one goal comes out as Highest priority at any given time
    //goal's priority can be dependant on external events or internal events.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(NpcGoalData npcGoalData in m_NpcGoals)
        {
            npcGoalData.evaluatePriority();
        }
        m_NpcGoals.OrderByDescending(x => x.GoalPriority);
        _NpcGoalData = m_NpcGoals.First();
    }

}
