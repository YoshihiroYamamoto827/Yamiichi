using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class MapPreviewer : EditorWindow
{
    //Jsonデータのディレクトリ
    private Object JsonDirectory;
    //オブジェクトデータのディレクトリ
    private Object ObjectDirectory;

    [MenuItem("Window/MapPreviewer")]
    static void Open()
    {
        var window = GetWindow<MapPreviewer>();
        window.titleContent = new GUIContent("MapPreview");
    }

    private void OnGUI()
    {
        //GUI上で表示
        GUILayout.BeginHorizontal();
        GUILayout.Label("Mapdata Directory:", GUILayout.Width(110));
        JsonDirectory = EditorGUILayout.ObjectField(JsonDirectory, typeof(UnityEngine.Object), true);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Objectdata Directory:", GUILayout.Width(110));
        ObjectDirectory = EditorGUILayout.ObjectField(ObjectDirectory, typeof(UnityEngine.Object), true);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void DrawMapPreviewButton()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Map Preview")) SceneManager.LoadScene("MapPreviewScene");
            EditorGUILayout.EndVertical();
    }
}
