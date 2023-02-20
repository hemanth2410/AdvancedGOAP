using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "New_Goap_Action",menuName ="GOAP/GOAP Action")]
public class Action : ScriptableObject
{
    [SerializeField] string m_actionName;
    [SerializeField] List<Conditions> m_PreConditions;
    [SerializeField] List<Conditions> m_PostConditions;
    [SerializeField] int m_Cost;
    [SerializeField] List<ExecutableAction> m_Actions;
    bool _actionFinished;
    public GameObject Agent;
    public string ActionName { get { return m_actionName;} }
    public List<Conditions> PreConditions { get { return m_PreConditions; } }
    public List<Conditions> PostConditions { get { return m_PostConditions;} }
    public int Cost { get { return m_Cost; } }
    public bool ActionFinished { get { return _actionFinished; } }
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
    /// Takes in necessary perameters to execute an action succesfully
    /// </summary>
    public void SetupAction(GameObject agent)
    {
        Agent = agent;
        foreach(ExecutableAction a in m_Actions)
        {
            a.OnExecuteBegin(Agent);
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
