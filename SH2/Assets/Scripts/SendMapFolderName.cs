using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SendMapFolderName : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Dropdown MCDropdown;
        List<string> MapList = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Map");
        FileInfo[] info = dir.GetFiles("*.");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);

            if(f.Name!="map")MapList.Add(f.Name);
        }

        //ドロップダウンのコンポーネント取得と表示するリストの宣言
        MCDropdown = GameObject.Find("MapChooseDropdown").GetComponent<Dropdown>();

        //ドロップダウンのOptionsをクリア
        MCDropdown.ClearOptions();

        //リストをドロップダウンに追加
        MCDropdown.AddOptions(MapList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
