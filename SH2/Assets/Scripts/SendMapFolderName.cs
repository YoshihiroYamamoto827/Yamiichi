using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class SendMapFolderName : MonoBehaviour
{
    /*GameObject DeveloperPanel;
    Dropdown MCDropdown;
    public static string MapFolderName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        //開発者設定のパネルの取得
        DeveloperPanel = GameObject.Find("MapSelectPanel");

        List<string> MapList = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Map");
        FileInfo[] info = dir.GetFiles("*.");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
            if (f.Name != "Map") MapList.Add(f.Name);
        }

        MCDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();

        //ドロップダウンのOptionsをクリア
        MCDropdown.ClearOptions();

        //リストをドロップダウンに追加
        MCDropdown.AddOptions(MapList);

        MCDropdown.options[MCDropdown.value].text = MapList[0];

        MapFolderName = MCDropdown.options[MCDropdown.value].text;

        DeveloperPanel.SetActive(false);
    }

    public void OnSelected()
    {
        MapFolderName = MCDropdown.options[MCDropdown.value].text;
        Debug.Log(MapFolderName);
    }

    public static string getMapFolderName()
    {
        Debug.Log(MapFolderName);
        return MapFolderName;
    }*/
}
