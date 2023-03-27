using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoalData : MonoBehaviour
{
    [SerializeField] protected string m_goalName;
    [SerializeField] protected float m_goalPriority;
    [SerializeField] protected goalType m_goalType;

    protected Dictionary<string, float> _believes;

    public string GoalName { get { return m_goalName; } }
    public float GoalPriority { get { return m_goalPriority; } }
    public goalType GoalType { get { return m_goalType; } }


    public virtual void GetBelieves()
    {
        _believes = GetComponent<NpcAgent>().BeliefDictionary;
    }

    public virtual void evaluatePriority()
    {
        // must execute code on all the child classes.
        // Priority = 1 - (health / maxHealth);
        // When player commmands to do certain thing. 
        // priority when core Priorities are meet;
    }
}
