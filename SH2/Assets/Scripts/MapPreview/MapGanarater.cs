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
        public Mapdata[] jsondata;
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
        Jsondata mapdata = new Jsondata();
        Jsondata inputjson = new Jsondata();

        mapdata.jsondata = new Mapdata[100];
        inputjson.jsondata = new Mapdata[100];

        inputFieldManager = GameObject.Find("InputFieldManager").GetComponent<InputFieldManager>();  
    }

    public void OnPreviewButton()
    {
        JsonFile = inputFieldManager.SendJsonFile();
        ImgDir = inputFieldManager.SendImgDirectory();

        string inputString = File.ReadAllText(JsonFile);

        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(inputString);
        Debug.Log(inputjson.jsondata[1]);
    }
}
