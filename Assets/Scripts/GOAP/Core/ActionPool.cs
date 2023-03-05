using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPool : MonoBehaviour
{
    [SerializeField] string ActionPoolName;
    [SerializeField] List<Action> actionList;
    public List<Action> ActionList { get { return actionList; } }

    private void Awake()
    {
        GOAPManager.Instance.RegisterNpcActionPool(this);
        Debug.Log("Registering Npc ActionPool");
    }
}
