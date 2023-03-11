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
            // Here each goal is evaluated based on its own Unique formula
            // Evaluating all the goals using same formula is meaningless
            // Because some goals are like booleans either on or off
            // some are like fuzzy machines, value goes up based on external factors.
            // the only bad part is if we are performing physics operations, this has to run on main thread.
            // how do i know which goal is of high priority when two goals have equal amount of priority
            npcGoalData.evaluatePriority();
        }
        m_NpcGoals.OrderByDescending(x => x.GoalPriority);
        _NpcGoalData = m_NpcGoals.First();
    }

}
