using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

//AssetBundleのビルドを行うウィンドウ
public class AssetBundleBuilder : EditorWindow
{
    [UnityEditor.MenuItem("Window/AssetBundleBuilder")]

    static void Open()
    {
        var window = GetWindow<AssetBundleBuilder>();
        window.titleContent = new GUIContent("AssetBundleBuild");
    }

    //ビルド対象のAsset、AssetBundleの名称、出力ディレクトリの変数
    private Object InputAsset, OutputDirectory;
    private string AssetBundleName;

    private void OnGUI()
    {
        //GUI上で表示
        GUILayout.BeginHorizontal();
        GUILayout.Label("Input Asset:", GUILayout.Width(110));
        InputAsset = EditorGUILayout.ObjectField(InputAsset, typeof(UnityEngine.Object), true);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("AssetBundleName:", GUILayout.Width(110));
        AssetBundleName = EditorGUILayout.TextField(AssetBundleName);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("OutputDirectory:", GUILayout.Width(110));
        OutputDirectory = EditorGUILayout.ObjectField(OutputDirectory, typeof(UnityEngine.Object), true);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if(GUILayout.Button("Build AssetBundle"))
        {
            BuildAssetBundle(InputAsset, AssetBundleName, OutputDirectory);
        }
    }

    public void BuildAssetBundle(Object Input, string Name, Object Output)
    {
        var builds = new List<AssetBundleBuild>();

        // AssetBundle名とそれに含めるアセットを指定する
        var build = new AssetBundleBuild();
        build.assetBundleName = "SampleJson4";
        build.assetNames = new string[1] {"Aseets/Mapdata/SampleJson4.json"};

        builds.Add(build);

        // 成果物を出力するフォルダを指定する（プロジェクトフォルダからの相対パス）
        var targetDir = "StreamingAssets";
        if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

        // Android用に出力
        var buildTarget = BuildTarget.Android;

        // LZ4で圧縮するようにする
        var buildOptions = BuildAssetBundleOptions.ChunkBasedCompression;

        BuildPipeline.BuildAssetBundles(targetDir, builds.ToArray(), buildOptions, buildTarget);
    }
}
