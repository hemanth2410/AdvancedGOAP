using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using UnityEditor.SceneManagement;
[RequireComponent(typeof(GoalManager))]
public class NpcAgent : MonoBehaviour
{
    [SerializeField] GoalDataStructure m_goalDataStructure;
    [SerializeField] NpcGoals _goals;
    [SerializeField] NpcGoals _liveAction;
    [SerializeField] Beliefs _beliefs;
    [SerializeField] List<string> believesNames;
    [SerializeField] float _health = 100.0f;
    [SerializeField] bool isMemoryCompatible;
    [SerializeField] int m_MaxItemsInMemory;
    ActionPool _actionPool;
    Planner _planner;
    float maxHealth = 0.0f;
    float _healthPriority = 0.0f;
    bool beginExecuteAction;
    Queue<Action> _actionQueue = new Queue<Action>();
    List<Action> _availableActions;
    NpcMemory _memory;
    Dictionary<string,float> goalDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _liveActionDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _beliefDictionary = new Dictionary<string,float>();
    Action currentAction;
    public Dictionary<string,float> BeliefDictionary { get { return _beliefDictionary; } }
    public GoalDataStructure GoalDataStructure { get { return m_goalDataStructure;} }
    // Start is called before the first frame update
    // we need a way to adjust goals and beliefs dynamically
    // I need to Adjust Beliefs and goals dynamically so that plannr can plan
    void Start()
    {
        if(isMemoryCompatible)
        {
            _memory = gameObject.AddComponent<NpcMemory>();
            _memory.MaximumNumberOfItemsInMemory = m_MaxItemsInMemory;
        }
        believesNames = new List<string>();
        _actionPool = GOAPManager.Instance.NpcActionPool;
        _availableActions = _actionPool.ActionList;
        for (int a = 0; a < _availableActions.Count; a++)
        {
            _availableActions[a].PerformPreSetup();
        }
        for (int i = 0; i < _goals.GoalList.Count; i++)
        {
            goalDictionary[_goals.GoalList[i].GoalName] = _goals.GoalList[i].GoalValue;
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
        if(beginExecuteAction && currentAction != null)
        {
            currentAction.ExecuteAction();
            if (currentAction.ActionFinished && _actionQueue.Count != 0)
            {
                currentAction = _actionQueue.Dequeue();
                believesNames.Clear();
                foreach (var v in currentAction.PostConditions)
                {
                    believesNames.Add(v.ConditionName);
                }
            } 
        }
        if(isMemoryCompatible)
        {
            _memory.UpdateTimers();
        }
    }
    private void FixedUpdate()
    {

        _liveAction.GoalList.Clear();
        _healthPriority = 1 - (_health / maxHealth);
        foreach(KeyValuePair<string,float> p in goalDictionary)
        {
            _liveActionDictionary[p.Key] = p.Value;
        }
        //_liveActionDictionary["Heal"] = _healthPriority; // need to get these values dynamically
        foreach(KeyValuePair<string,float> v in _liveActionDictionary)
        {
            Goal g = new Goal();
            g.GoalName = v.Key;
            g.GoalValue = v.Value;
            _liveAction.GoalList.Add(g);
        }
        if(currentAction != null && !currentAction.ActionFinished)
        {
            return;
        }
        // define goals dictionary here, This is modified every fixed update for now
        // A priority system will be implemented soon
        if (_planner == null || _actionQueue == null)
        {
            beginExecuteAction = false;
            _planner = new Planner();
            var _prioritizedGoal = from entry in _liveActionDictionary orderby entry.Value descending select entry;
            var firstGoal = _prioritizedGoal.First();
            Dictionary<string, float> _goalDictionary = new Dictionary<string, float>
            {
                { firstGoal.Key, firstGoal.Value }
            };
            _actionQueue = _planner.Plan(_availableActions, _goalDictionary, _beliefDictionary);
            if(_actionQueue != null)
            {
                Debug.Log("Current goal = " + firstGoal.Key);
                foreach (Action a in _actionQueue)
                {
                    a.SetupAction(gameObject);
                }
            }
        }
        if(_actionQueue != null && _actionQueue.Count > 0)
        {
            
            currentAction = _actionQueue.Dequeue();
            foreach (var v in currentAction.PostConditions)
            {
                believesNames.Add(v.ConditionName);
            }
            if (currentAction.PreperformAction())
            {
                beginExecuteAction = true;
            }
            else
            {
                _actionQueue = null;
                beginExecuteAction = false;
                currentAction = null;
            }
        }
    }

    public void ModifyHealth(float value)
    {
        _health = Mathf.Clamp(_health + value,0,maxHealth);
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