using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New_Goap_Action",menuName ="GOAP/GOAP Action")]
public class Action : ScriptableObject
{
    [SerializeField] string m_actionName;
    [SerializeField] List<Conditions> m_PreConditions;
    [SerializeField] List<Conditions> m_PostConditions;
    [SerializeField] int m_Cost;
    [SerializeField] List<ExecutableAction> m_Actions;
    [SerializeField] bool m_isBreakable;
    Dictionary<string, float> _preConditionsDictionary = new Dictionary<string, float>();
    Dictionary<string, float> _afterEffects = new Dictionary<string, float>();
    bool _actionFinished;
    public GameObject Agent;
    public string ActionName { get { return m_actionName;} }
    public List<Conditions> PreConditions { get { return m_PreConditions; } }
    public List<Conditions> PostConditions { get { return m_PostConditions;} }
    public Dictionary<string, float> PreConditionsDictionary { get { return _preConditionsDictionary; } }
    public Dictionary<string, float> AfterEffectsDictionary { get { return _afterEffects; } }
    public int Cost { get { return m_Cost; } }
    public bool ActionFinished { get { return _actionFinished; } }
    public bool IsBreakable { get { return m_isBreakable; } }
    /// <summary>
    /// This function executes the given action
    /// </summary>
    public void ExecuteAction()
    {
        foreach(ExecutableAction a in m_Actions)
        {
            a.ExecuteAction();
        }
        _actionFinished = m_Actions.All(x => x.IsSubActionRunning == false);
    }
    /// <summary>
    /// Takes in necessary perameters to execute an action succesfully & populates the conditions dictionary.
    /// </summary>
    public void SetupAction(GameObject agent)
    {
        
        Agent = agent;
        foreach(ExecutableAction a in m_Actions)
        {
            a.OnExecuteBegin(Agent);
        }
    }
    public void PerformPreSetup()
    {
        foreach (var a in m_PreConditions)
        {
            _preConditionsDictionary[a.ConditionName] = a.ConditionValue;
        }
        foreach (var a in m_PostConditions)
        {
            _afterEffects[a.ConditionName] = a.ConditionValue;
        }
    }
    public bool PreperformAction()
    {
        return true;
    }
    public void PostPerformAction()
    {
        // Remove belief here
        NpcAgent _a = Agent.GetComponent<NpcAgent>();
        foreach(KeyValuePair<string, float> var in _preConditionsDictionary)
        {
            try
            {
                _a.RemoveBelief(var.Key, var.Value);
            }
            catch(Exception ex)
            {
                Debug.LogError("This is what is fucked up : " + ex);
            }
        }
    }
}

[System.Serializable]
public class Conditions
{
    [SerializeField] string conditionName;
    [SerializeField] int conditionValue;

    public string ConditionName { get { return conditionName;} }
    public int ConditionValue { get { return conditionValue;} }
}
