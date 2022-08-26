using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow
{
    //ウィンドウを表示するサンプル
    [MenuItem("Window/MyWindow")]
    static void Open()
    {
        var window = GetWindow<MyWindow>();
        window.titleContent = new GUIContent("オリジナルのウィンドウ");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("オリジナルのウィンドウを作ろう");
            EditorGUILayout.LabelField("EditorGUILayout.BeginVerticalを使うと縦に並びます");
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("EditorGUILayout.BeginVerticalを使えば");
                EditorGUILayout.LabelField("横に並べることもできます");
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("こちらはスタイルなしバージョン");
            EditorGUILayout.LabelField("周りが囲われていません");
        EditorGUILayout.EndVertical();
    }
}
