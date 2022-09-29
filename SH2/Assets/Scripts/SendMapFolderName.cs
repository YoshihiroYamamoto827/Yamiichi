using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.IO;

public class SendMapFolderName : MonoBehaviour
{
    GameObject DeveloperPanel;
    TMP_Dropdown MCDropdown;
    public static string MapFolderName;

    // Start is called before the first frame update
    void Start()
    {
        //開発者設定のパネルの取得
        DeveloperPanel = GameObject.Find("DeveloperPanel");

        List<string> MapList = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Map");
        FileInfo[] info = dir.GetFiles("*.");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
            if(f.Name!="Map")MapList.Add(f.Name);
        }

        //ドロップダウンのコンポーネント取得と表示するリストの宣言
        MCDropdown = GameObject.Find("MapChooseDropdown").GetComponent<TMP_Dropdown>();
        Debug.Log(MCDropdown);

        //ドロップダウンのOptionsをクリア
        MCDropdown.ClearOptions();

        //リストをドロップダウンに追加
        MCDropdown.AddOptions(MapList);

        MCDropdown.options[MCDropdown.value].text = MapList[0];

        DeveloperPanel.SetActive(false);
    }

    public void OnSelected()
    {
        MapFolderName = MCDropdown.options[MCDropdown.value].text;
        Debug.Log(MapFolderName);
    }

    public static string getMapFolderName()
    {
        return MapFolderName;
    }
}
