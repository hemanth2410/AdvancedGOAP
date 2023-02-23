using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NPC Goals", menuName = "GOAP/GOAP Goals/NPC generic")]
public class GoalDataStructure : ScriptableObject
{
    [SerializeField] List<GoalData> NpcGoals;
    public List<GoalData> Goals { get { return NpcGoals; } }
    public void SetGoals(List<GoalData> goals)
    {
        NpcGoals = new List<GoalData>();
        NpcGoals = goals;
    }
}

[System.Serializable]
public class GoalData
{
    [SerializeField] string goalName;
    [SerializeField] float priority;
    [SerializeField] UnityEvent triggerEvent;
    public string GoalName { get { return goalName; } }
    public float Priority { get { return priority; } }
    public UnityEvent Triggers { get { return triggerEvent; } }

    public void setGoal(string name, float value, UnityEvent trigger)
    {
        goalName = name;
        priority = value;
        triggerEvent = trigger;
    }
}