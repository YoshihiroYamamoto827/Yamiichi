using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

//MapGenerater
public class MapGenerator : MonoBehaviour
{
    //jsonファイルから入力される配列
    [System.Serializable]
    public class Jsondata
    {
        public Mapdata[] mapdata;
    }

    //jsonデータのフォーマット
    [System.Serializable]
    public class Mapdata
    {
        public int xcoor;
        public int ycoor;
        public string objectname;
    }

    [System.Serializable]
    public class MapInfo
    {
        public int mapsize;
        public string date;
    }

    //AssetBundleファイルの指定 
    private string MapFolderName;
    //マップの大きさ
    private int mapSize;
    //マップデータを代入する変数
    private Jsondata jsondata;
    //マップ生成時に対象となるprefabを指す変数
    private GameObject InstanceObject;
    //マップ生成時にy座標を代入する変数
    private float y;
    //マップ生成時に各オブジェクトの親となる空オブジェクトを指す変数
    private GameObject Parent;

    private void Start()
    {
        init();
        MapFolderName = "Sample1";
        MapGenerate(MapFolderName);
    }

    private void init()
    {
        y = 0;
        MapInfo info = new MapInfo();
    }

    public void MapGenerate(string MapFolderName)
    {
        //アセットバンドルの宣言と初期化
        AssetBundle JsonAssetBundle, ObjectAssetBundle, ManagerAssetBundle;
        GameObject Wall, Door, Floor, Item, Player, SceneManager, ItemManager;

        //JsonファイルのAssetBundleとAssetBundle内の対応するJsonファイルの読み込み
        JsonAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Map/" + MapFolderName);
        string mapdatainputString = JsonAssetBundle.LoadAsset<TextAsset>("Assets/MapData/" + MapFolderName + "/Mapdata.json").ToString();
        string mapinfoinputString = JsonAssetBundle.LoadAsset<TextAsset>("Assets/MapData/" + MapFolderName + "/MapInfo.json").ToString();

        //マップサイズ情報をクラス変数に代入
        MapInfo inputjson2 = JsonUtility.FromJson<MapInfo>(mapinfoinputString);

        //マップデータのクラスを初期化、マップサイズ変数で配列を宣言
        jsondata = new Jsondata();
        mapSize = inputjson2.mapsize;
        jsondata.mapdata = new Mapdata[mapSize];

        //マップのjsonファイルのデータをクラス変数に代入
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(mapdatainputString);

        //Debug.Log(inputjson.mapdata[0].xcoor);

        //オブジェクト、MagagerのAssetBundleの読み込み
        ObjectAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Objects");
        ManagerAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Manager");

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Wall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        Player = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Player.prefab");
        SceneManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/SceneManager.prefab");
        ItemManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/ItemManager.prefab");

        //各オブジェクトの生成
        GameObject WallParent = new GameObject("Walls");
        GameObject DoorParent = new GameObject("Doors");
        GameObject FloorParent = new GameObject("Floors");
        GameObject ItemParent = new GameObject("Items");

        ObjectInstance1(SceneManager);
        ObjectInstance1(ItemManager);
        for (int i = 0; i < mapSize * mapSize; i++)
        {
            if (inputjson.mapdata[i].objectname != "")
            {
                switch (inputjson.mapdata[i].objectname)
                {
                    case "Capture\\001.png":
                        InstanceObject = Wall;
                        y = 2f;
                        Parent = WallParent;
                        break;

                    case "Capture\\002.png":
                        InstanceObject = Door;
                        y = 1.5f;
                        Parent = DoorParent;
                        break;

                    case "Capture\\003.png":
                        InstanceObject = Wall;
                        y = 1f;
                        Parent = WallParent;
                        break;

                    case "Capture\\004.png":
                        InstanceObject = Wall;
                        y = 1f;
                        Parent = WallParent;
                        break;

                    case "Capture\\005.png":
                        InstanceObject = Item;
                        y = 1.5f;
                        Parent = ItemParent;
                        break;

                    case "Capture\\006.png":
                        InstanceObject = Player;
                        y = 1.5f;
                        Parent = null;
                        break;
                }
                ObjectInstance2(Floor,FloorParent,inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor,InstanceObject, Parent);
            }
            var floor = Instantiate(Floor, new Vector3(inputjson.mapdata[i].xcoor, 0f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
            floor.name = Floor.name;
            if (FloorParent != null) floor.transform.parent = FloorParent.transform;
        }
    }

    private void ObjectInstance1(GameObject objb)
    {
       var obja = Instantiate(objb, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        obja.name = objb.name;
    }

    private void ObjectInstance2(GameObject floorb, GameObject floorparent, float x,float y, float z, GameObject objb, GameObject parent)
    {
        var floora = Instantiate(floorb, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
        floora.name = floorb.name;
        if (floorparent != null) floora.transform.parent = floorparent.transform;

        var obja = Instantiate(objb, new Vector3(x, y, z), Quaternion.identity) as GameObject;
        obja.name = objb.name;
        if (parent != null) obja.transform.parent = parent.transform;
    }
}