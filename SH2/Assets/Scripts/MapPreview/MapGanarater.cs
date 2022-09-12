using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGanarater : MonoBehaviour
{
    InputFieldManager inputFieldManager;
    private string JsonFile, ImgDir;

    [System.Serializable]
    public class Jsondata
    {
        public Mapdata[] mapdata;
    }

    [System.Serializable]
    public class Mapdata
    {
        public int xcoor;
        public int ycoor;
        public string objectname;
    }

    void Start()
    {
        Jsondata jsondata = new Jsondata();
        jsondata.mapdata = new Mapdata[100];

        inputFieldManager = GameObject.Find("InputFieldManager").GetComponent<InputFieldManager>();  
    }

    public void OnPreviewButton()
    {
        //JsonファイルのAssetBundleとAssetBundle内の対応するJsonファイルの読み込み
        var JsonassetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/samplejson5");
        string inputString = JsonassetBundle.LoadAsset<TextAsset>("Assets/MapData/SampleJson5.json").ToString();

        //Jsonファイルのデータをクラス変数に代入
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(inputString);

        Debug.Log(inputjson.mapdata[0].xcoor);

        //オブジェクトのprefabが入っているフォルダのAssetBundleの読み込み、AssetBundle内から各オブジェクトの変数に代入
        var ObjectAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/objects");
        var Wall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Wall.prefab");
        var Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        var Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        var Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        var Player = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Player.prefab");

        for(int i = 0; i < 100; i++)
        {
            Instantiate(Floor, new Vector3(inputjson.mapdata[i].xcoor, 0f, inputjson.mapdata[i].ycoor),Quaternion.identity);

            if(inputjson.mapdata[i].objectname != null)
            {
                switch(inputjson.mapdata[i].objectname){
                    case "Capture\\001.png":
                        Instantiate(Wall, new Vector3(inputjson.mapdata[i].xcoor, 2f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        break;
                    case "Capture\\002.png":
                        Instantiate(Door, new Vector3(inputjson.mapdata[i].xcoor, 1.5f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        break;
                    //case "Capture\\003.png":
                    //Instantiate(Wall, new Vector3(inputjson.mapdata[i].xcoor, 1f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                    //break;
                    //case "Capture\\004.png":
                    //Instantiate(Wall, new Vector3(inputjson.mapdata[i].xcoor, 1f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                    //break;
                    case "Capture\\005.png":
                        Instantiate(Item, new Vector3(inputjson.mapdata[i].xcoor, 1.5f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        break;
                    case "Capture\\006.png":
                        Instantiate(Player, new Vector3(inputjson.mapdata[i].xcoor, 1.5f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        break;
                }
            }
        }
    }
}
