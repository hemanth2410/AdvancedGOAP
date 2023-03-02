using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPool : MonoBehaviour
{
    [SerializeField] List<Action> actionList;
    public List<Action> ActionList { get { return actionList; } }
}
