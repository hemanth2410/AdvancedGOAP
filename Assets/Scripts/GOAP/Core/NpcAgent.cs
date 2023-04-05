using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using UnityEditor.SceneManagement;
[RequireComponent(typeof(GoalManager))]
public class NpcAgent : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GoalDataStructure m_goalDataStructure;
    [Header("NPC mind")]
    [SerializeField][Range(0,1)] float m_Killer;
    [SerializeField][Range(0,1)] float m_Achiever;
    [SerializeField][Range(0,1)] float m_Socializer;
    [SerializeField][Range(0,1)] float m_Explorer;
    [Space]
    [SerializeField] NpcGoals _goals;
    [SerializeField] NpcGoals _liveAction;
    [SerializeField] Beliefs _beliefs;
    [SerializeField] List<string> believesNames;
    [SerializeField] float _health = 100.0f;
    [SerializeField] float _energy = 100.0f;
    [SerializeField] bool isMemoryCompatible;
    [SerializeField] int m_MaxItemsInMemory;
    ActionPool _actionPool;
    Planner _planner;
    float maxHealth = 0.0f;
    float energy = 0.0f;
    float _healthPriority = 0.0f;
    bool beginExecuteAction;
    Queue<Action> _actionQueue = new Queue<Action>();
    List<Action> _availableActions;
    NpcMemory _memory;
    GoalManager _goalManager;
    Dictionary<string, float> playerMind = new Dictionary<string, float>();
    Dictionary<string,float> goalDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _liveActionDictionary = new Dictionary<string,float>();
    Dictionary<string,float> _beliefDictionary = new Dictionary<string,float>();
    Action currentAction;
    public Dictionary<string,float> BeliefDictionary { get { return _beliefDictionary; } }
    public GoalDataStructure GoalDataStructure { get { return m_goalDataStructure;} }
    public Dictionary<string,float> PlayerMind { get { return  playerMind; } }
    public float Energy { get { return energy; } }
    NpcGoalData _currentGoal;
    #endregion
    // Start is called before the first frame update
    // we need a way to adjust goals and beliefs dynamically
    // I need to Adjust Beliefs and goals dynamically so that plannr can plan
    private void Awake()
    {
        if (isMemoryCompatible)
        {
            _memory = gameObject.AddComponent<NpcMemory>();
            _memory.MaximumNumberOfItemsInMemory = m_MaxItemsInMemory;
        }
    }
    void Start()
    {
        //setup NPC mind
        m_Killer = Random.Range(0.0f, 1.0f);
        m_Achiever = Random.Range(0.0f, 1.0f);
        m_Socializer = 1 - m_Killer;
        m_Explorer = 1 - m_Achiever;
        playerMind["Killer"] = m_Killer;
        playerMind["Achiever"] = m_Achiever;
        playerMind["Socializer"] = m_Socializer;
        playerMind["Explorer"] = m_Explorer;
        
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
        energy = _energy;
        _healthPriority = 1 - (_health / maxHealth);
        _goalManager = GetComponent<GoalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //_health -= Time.deltaTime;
        //_beliefDictionary["Health"] = _health;
        if (beginExecuteAction && currentAction != null)
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

        //_liveAction.GoalList.Clear();
        //_healthPriority = 1 - (_health / maxHealth);
        //foreach(KeyValuePair<string,float> p in goalDictionary)
        //{
        //    _liveActionDictionary[p.Key] = p.Value;
        //}
        ////_liveActionDictionary["Heal"] = _healthPriority; // need to get these values dynamically
        //foreach(KeyValuePair<string,float> v in _liveActionDictionary)
        //{
        //    Goal g = new Goal();
        //    g.GoalName = v.Key;
        //    g.GoalValue = v.Value;
        //    _liveAction.GoalList.Add(g);
        //}
        var _highPriority = _goalManager.NpcGoalData;
        if(currentAction != null && !currentAction.ActionFinished && _currentGoal.GoalName == _highPriority.GoalName)
        {
            return;
        }
        // define goals dictionary here, This is modified every fixed update for now
        // A priority system will be implemented soon
        if (_planner == null || _actionQueue == null || _actionQueue.Count == 0 || _highPriority.GoalName != _currentGoal.GoalName)
        {
            beginExecuteAction = false;
            _planner = new Planner();
            var _prioritizedGoal = _goalManager.NpcGoalData;
            _currentGoal = _prioritizedGoal;
            if (_prioritizedGoal == null)
                return;
            Dictionary<string, float> _goalDictionary = new Dictionary<string, float>
            {
                { _prioritizedGoal.GoalName, _prioritizedGoal.GoalPriority }
            };
            _actionQueue = _planner.Plan(_availableActions, _goalDictionary, _beliefDictionary);
            if(_actionQueue != null)
            {
                Debug.Log("Current goal = " + _prioritizedGoal.GoalName);
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
    public void ModifyEnergy(float value)
    {
        energy = Mathf.Clamp(value, 0.0f, 100.0f);
    }
    public void ModifyHealth(float value)
    {
        _health = Mathf.Clamp(_health + value,0,maxHealth);
    }
    public void InjectBelief(string Name, float value)
    {
        float _value = 0.0f;
        if(_beliefDictionary.TryGetValue(Name, out _value))
        {
            _beliefDictionary[Name] = value;
            return;
        }
        _beliefDictionary.Add(Name, value);
    }
    public void RemoveBelief(string name,  float value)
    {
        _beliefDictionary.Remove(name);
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