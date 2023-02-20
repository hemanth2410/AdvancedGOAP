using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName ="New Animation Executable", menuName = "GOAP/GOAP Action Executable")]
public class ExecutableAnimation : ExecutableAction
{
    [SerializeField] AnimatorPerameters m_AnimatorPerameters;

    //Get reference from attached gameObject
    Animator _animator;
    public override void OnExecuteBegin()
    {
        base.OnExecuteBegin();
        m_AnimatorPerameters.PopulateDictionaries();
        _animator = target.GetComponent<Animator>();
    }

    public override void ExecuteAction(GameObject agent)
    {
        base.ExecuteAction(agent);
        for (int i = 0; i < m_AnimatorPerameters.TriggerPerameters.Count; i++)
        {
            _animator.SetTrigger(m_AnimatorPerameters.TriggerPerameters[i].PerameterName);
        }
        foreach(var intPerameter in m_AnimatorPerameters.IntPerameters)
        {
            _animator.SetInteger(intPerameter.Key, intPerameter.Value);
        }
        foreach(var floatPerameter in m_AnimatorPerameters.FloatPerameters)
        {
            _animator.SetFloat(floatPerameter.Key, floatPerameter.Value);
        }
        foreach(var boolPerameter in m_AnimatorPerameters.BoolPerameters)
        {
            _animator.SetBool(boolPerameter.Key, boolPerameter.Value);
        }
    }

    public override void OnExecuteEnd()
    {
        base.OnExecuteEnd();
    }
}

[System.Serializable]
public class AnimatorPerameters
{
    [SerializeField] List<IntPerameters> m_IntPerameters;
    [SerializeField] List<FloatPerameters> m_FloatPerameters;
    [SerializeField] List<BoolPerameters> m_BoolPerameters;
    [SerializeField] List<TriggerPerameters> m_TriggerPerameters;

    public Dictionary<string, int> IntPerameters = new Dictionary<string, int>();
    public Dictionary<string, float> FloatPerameters = new Dictionary<string, float>();
    public Dictionary<string, bool> BoolPerameters = new Dictionary<string, bool>();
    public List<TriggerPerameters> TriggerPerameters { get { return m_TriggerPerameters; } }

    public void PopulateDictionaries()
    {
        IntPerameters = m_IntPerameters.ToDictionary(X => X.PerameterName, X => X.PerameterValue);
        FloatPerameters = m_FloatPerameters.ToDictionary(F => F.PerameterName, F => F.PerameterValue);
        BoolPerameters = m_BoolPerameters.ToDictionary(B => B.PerameterName, B => B.PerameterValue);
    }
}

[System.Serializable]
public class IntPerameters
{
    [SerializeField] string perameterName;
    [SerializeField] int perameterValue;

    public string PerameterName { get { return perameterName; } }
    public int PerameterValue { get { return perameterValue; } }
}

[System.Serializable]
public class FloatPerameters
{
    [SerializeField] string perameterName;
    [SerializeField] float perameterValue;

    public string PerameterName { get { return perameterName; } }
    public float PerameterValue { get { return perameterValue; } }
}

[System.Serializable]
public class BoolPerameters
{
    [SerializeField] string perameterName;
    [SerializeField] bool perameterValue;

    public string PerameterName { get { return perameterName; } }
    public bool PerameterValue { get { return perameterValue; } }
}
[System.Serializable]
public class TriggerPerameters
{
    [SerializeField] string perameterName;
    public string PerameterName { get { return perameterName; } }
}