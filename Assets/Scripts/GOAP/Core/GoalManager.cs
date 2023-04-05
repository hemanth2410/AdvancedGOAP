using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(NpcAgent))]
public class GoalManager : MonoBehaviour
{
    [SerializeField] List<NpcGoalData> m_NpcGoals;
    [SerializeField] List<NpcGoalData> m_Goals;
    [SerializeField] NpcGoalData _NpcGoalData;
    [SerializeField] goalType m_npcType;
    public List<NpcGoalData> NpcGoals { get { return m_NpcGoals; } }
    public NpcGoalData NpcGoalData { get { return _NpcGoalData; } }
    //This class should be able to dynamically adjust the goal on priority
    //and needs to make sure that only one goal comes out as Highest priority at any given time
    //goal's priority can be dependant on external events or internal events.
    // Start is called before the first frame update
    void Start()
    {
        NpcGoalData[] _goals = GetComponents<NpcGoalData>();
        m_NpcGoals = _goals.ToList();
        for (int i = 0; i < _goals.Length; i++)
        {
            _goals[i].GetBelieves();
        }
        //Evaluating NPCtype here
        string npcType = GetComponent<NpcAgent>().PlayerMind.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        if (npcType == "Killer") m_npcType = goalType.Killer;
        if (npcType == "Achiever") m_npcType = goalType.Achiever;
        if (npcType == "Socializer") m_npcType = goalType.Socializer;
        if (npcType == "Explorer") m_npcType = goalType.Explorer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach (NpcGoalData npcGoalData in m_NpcGoals)
        {
            // Here each goal is evaluated based on its own Unique formula
            // Evaluating all the goals using same formula is meaningless
            // Because some goals are like booleans either on or off
            // some are like fuzzy machines, value goes up based on external factors.
            // the only bad part is if we are performing physics operations, this has to run on main thread.
            // how do i know which goal is of high priority when two goals have equal amount of priority
            // So we can code a custom priority system for each goal. that adds to master priority after that we can evaluate these values
            npcGoalData.evaluatePriority();
        }
        m_Goals = m_NpcGoals.OrderByDescending(x => x.GoalPriority).ToList();
        // get first goal that allighs with the NPCmind
        for (int i = 0; i < m_NpcGoals.Count; i++)
        {
            if (m_Goals[i].GoalType == m_npcType && m_NpcGoals[i].GoalPriority > 0.75f)
            {
                _NpcGoalData = m_Goals[i];
                return;
            }
        }
        _NpcGoalData = m_Goals.First();
    }

}
