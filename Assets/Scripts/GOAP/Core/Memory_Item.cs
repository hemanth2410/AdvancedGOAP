using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Memory_Item : MonoBehaviour
{
    [SerializeField] float Time;
    public float m_Time { get { return Time; } }
  
}
