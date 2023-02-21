using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CustomEditor(typeof(NpcAgent))]
public class GOPAagentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Show Prioritizer"))
        {
            EditorWindow.GetWindow<GoalPrioritizerEditor>();
        }
    }
}
