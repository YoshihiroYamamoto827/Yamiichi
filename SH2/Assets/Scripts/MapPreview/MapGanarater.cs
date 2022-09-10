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
        var assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/samplejson4");
        string inputString = assetBundle.LoadAsset<TextAsset>("Assets/MapData/SampleJson4.json").ToString();


        JsonFile = inputFieldManager.SendJsonFile();
        ImgDir = inputFieldManager.SendImgDirectory();

        //string inputString = MapData.Load<TextAsset>("SampleJson4");

        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(inputString);
        Debug.Log(inputjson.mapdata[1]);
    }
}
