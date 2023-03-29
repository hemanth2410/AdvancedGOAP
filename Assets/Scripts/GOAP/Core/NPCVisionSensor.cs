using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SphereCollider))]
public class NPCVisionSensor : MonoBehaviour
{
    #region Inspector Assigned Variables
    [SerializeField][Range(0, 360)] float m_FOV = 120f;
    [SerializeField] float m_radius;
    [SerializeField] LayerMask m_objectsOfInterests;
    #endregion
    #region Private Variables
    SphereCollider _sphereCollider;
    NpcMemory _npcMemory;
    #endregion
    #region Public properties
    public float FOV { get { return m_FOV; }}
    public float Radius { get { return m_radius;} }
    #endregion

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = m_radius;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        _npcMemory = GetComponentInParent<NpcMemory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Collider[] collidersOfInterest = Physics.OverlapSphere(transform.position, m_radius, m_objectsOfInterests);
        foreach(Collider collider in collidersOfInterest)
        {
            if(Mathf.Abs(Vector3.Angle(transform.forward, (collider.transform.position-transform.position).normalized))<FOV/2.0f)
            {
                if(collider.GetComponent<Memory_Item>())
                {
                    MemoryItem newItem = new MemoryItem(collider.gameObject, collider.GetComponent<Memory_Item>().m_Time);
                    _npcMemory.AddMemoryItem(newItem);
                }
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
[CustomEditor(typeof(NPCVisionSensor))]
public class NPCVisionSensor_Editor : Editor
{

    private void OnSceneGUI()
    {
        NPCVisionSensor sensor = (NPCVisionSensor)target;
        //Handles.DrawSolidArc(sensor.transform.position,sensor.transform.up, sensor.transform.forward, sensor.FOV,sensor.Radius);
        Vector3 Direction1 = DirectionFromAngle(sensor.transform.eulerAngles.y, -sensor.FOV / 2.0f);
        Vector3 Direction2 = DirectionFromAngle(sensor.transform.eulerAngles.y, sensor.FOV / 2.0f);
        Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Handles.DrawSolidArc(sensor.transform.position, sensor.transform.up, Direction1, sensor.FOV/2, sensor.Radius);
        Handles.DrawSolidArc(sensor.transform.position, sensor.transform.up, Direction2, -sensor.FOV/2, sensor.Radius);
    }
    private Vector3 DirectionFromAngle(float eularY, float angleInDegrees)
    {
        angleInDegrees += eularY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0 , Mathf.Cos(angleInDegrees* Mathf.Deg2Rad));
    }
}