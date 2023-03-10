using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoalData : MonoBehaviour
{
    [SerializeField] protected string m_goalName;
    [SerializeField] protected float m_goalPriority;

    public string GoalName { get { return m_goalName; } }
    public float GoalPriority { get { return m_goalPriority; } }

    public virtual void evaluatePriority()
    {
        // must execute code on all the child classes.
    }
}
