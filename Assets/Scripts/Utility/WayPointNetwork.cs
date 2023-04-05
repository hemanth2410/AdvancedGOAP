using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class WayPointNetwork : MonoBehaviour
{
    Transform[] _waypoints;
    public Transform[] Waypoints { get { return _waypoints; } }
    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetWaypoints()
    {
        _waypoints = GetComponentsInChildren<Transform>();
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (i == _waypoints.Length - 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[0].position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(WayPointNetwork))]
public class WayPointNetworkEditor : Editor
{
    private void OnSceneGUI()
    {
        WayPointNetwork w = (WayPointNetwork)target;
        w.GetWaypoints();
    }
}
#endif