using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GOAPManager : MonoBehaviour
{
    static GOAPManager _manager;
    public static GOAPManager Instance
    {
        get
        {
            if (_manager == null)
            {
                _manager = FindObjectOfType<GOAPManager>();
            }
            return _manager;
        }
    }

    ActionPool _pool;
    public ActionPool NpcActionPool { get { return _pool; } } 

    public void RegisterNpcActionPool(ActionPool pool)
    {
        _pool = pool;
    }
}
