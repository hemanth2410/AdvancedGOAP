using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class StateTester : MonoBehaviour
{
    [SerializeField] List<Action> actionList = new List<Action>();
    [SerializeField] bool ActionFinished;
    [SerializeField] string currentActionName;
    Action _currentAction;
    Queue<Action> _actionQueue;
    // Start is called before the first frame update
    void Start()
    {
        _actionQueue = new Queue<Action>();
        for (int i = 0; i < actionList.Count; i++)
        {
            actionList[i].SetupAction(gameObject);
            _actionQueue.Enqueue(actionList[i]);
        }
        currentActionName = actionList[0].ActionName;
        _currentAction = _actionQueue.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        _currentAction.ExecuteAction();
        if(_currentAction.ActionFinished)
        {
            _currentAction = _actionQueue.Dequeue();
            currentActionName = _currentAction.ActionName;
        }
    }
}
