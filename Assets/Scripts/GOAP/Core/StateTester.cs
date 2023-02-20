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
            Action _executableAction = actionList[i];
            _executableAction.SetupAction(gameObject);
            _actionQueue.Enqueue(_executableAction);
        }
        _currentAction = _actionQueue.Dequeue();
        currentActionName = _currentAction.ActionName;
    }

    // Update is called once per frame
    void Update()
    {
        _currentAction.ExecuteAction();
        if(_currentAction.ActionFinished && _actionQueue.Count != 0)
        {
            _currentAction = _actionQueue.Dequeue();
            currentActionName = _currentAction.ActionName;
        }
    }
}
