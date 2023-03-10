using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] NpcGoals m_NpcGoals;
    //This class should be able to dynamically adjust the goal on priority
    //and needs to make sure that only one goal comes out as Highest priority at any given time
    //goal's priority can be dependant on external events or internal events.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
