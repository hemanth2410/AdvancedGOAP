using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAgent : MonoBehaviour
{
    [SerializeField] GoalDataStructure m_goalDataStructure;
    [SerializeField] NpcGoals _goals;
    [SerializeField] NpcGoals _liveAction;
    [SerializeField] Beliefs _beliefs;
    [SerializeField] float _health = 100.0f;
    float maxHealth = 0.0f;
    float _healthPriority = 0.0f;
    Dictionary<string,float> _goalDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _liveActionDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _beliefDictionary = new Dictionary<string,float>();

    public Dictionary<string,float> BeliefDictionary { get { return _beliefDictionary; } }
    public GoalDataStructure GoalDataStructure { get { return m_goalDataStructure;} }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _goals.GoalList.Count; i++)
        {
            _goalDictionary[_goals.GoalList[i].GoalName] = _goals.GoalList[i].GoalValue;
        }
        //populate beliefs
        for (int j = 0; j < _beliefs.Believes.Count; j++)
        {
            _beliefDictionary[_beliefs.Believes[j].Name] = _beliefs.Believes[j].Value;
        }
        maxHealth = _health;
        _healthPriority = 1 - (_health / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        _health -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        _liveAction.GoalList.Clear();
        _healthPriority = 1 - (_health / maxHealth);
        foreach(KeyValuePair<string,float> p in _goalDictionary)
        {
            _liveActionDictionary[p.Key] = p.Value;
        }
        _liveActionDictionary["Heal"] = _healthPriority; // need to get these values dynamically
        foreach(KeyValuePair<string,float> v in _liveActionDictionary)
        {
            Goal g = new Goal();
            g.GoalName = v.Key;
            g.GoalValue = v.Value;
            _liveAction.GoalList.Add(g);
        }
    }
}

[System.Serializable]
public class NpcGoals
{
    [SerializeField] List<Goal> goals;
    public List<Goal> GoalList { get { return goals; } }
}

[System.Serializable]
public class Goal
{
    public string GoalName;
    [Range(0.0f,1.0f)]public float GoalValue;
}
[System.Serializable]
public class Beliefs
{
    [SerializeField] List<Belief> m_believes;
    public List<Belief> Believes { get { return m_believes; } }
}
[System.Serializable]
public class Belief
{
    public string Name;
    public float Value;
}