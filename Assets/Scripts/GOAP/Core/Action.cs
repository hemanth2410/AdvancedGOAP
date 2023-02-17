using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New_Goap_Action",menuName ="GOAP/GOAP Action")]
public class Action : ScriptableObject
{
    [SerializeField] string m_actionName;
    [SerializeField] List<Conditions> m_PreConditions;
    [SerializeField] List<Conditions> m_PostConditions;
    [SerializeField] int m_Cost;
    [SerializeField] ExecutableAction m_Action;


    public string ActionName { get { return m_actionName;} }
    public List<Conditions> PreConditions { get { return m_PreConditions; } }
    public List<Conditions> PostConditions { get { return m_PostConditions;} }
    public int Cost { get { return m_Cost; } }
    /// <summary>
    /// This function executes the given action
    /// </summary>
    public void ExecuteAction()
    {
        m_Action.ExecuteAction();
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
