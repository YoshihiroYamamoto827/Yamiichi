using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class hoge : EditorWindow
{
    [MenuItem("Window/hoge")]
    private static void Open()
    {
        var window = GetWindow<hoge>("test");
    }

    private Vector2 scrollposition = Vector2.zero;

    private void OnGUI()
    {
        scrollposition = EditorGUILayout.BeginScrollView(scrollposition);

        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        if (GUILayout.Button("hoge")) Debug.Log("hoge");
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("hoge")) Debug.Log("hoge");
    }
}
