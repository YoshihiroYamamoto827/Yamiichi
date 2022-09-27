using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

//MapGenerater
public class MapGenerator : MonoBehaviour
{
    //jsonファイルのテンプレート
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

    [System.Serializable]
    public class MapInfo
    {
        public int mapsize;
        public string date;
    }

    //AssetBundleを読み込むフォルダの名前 
    private string MapFolderName;
    //マップの大きさ
    private int mapSize;
    //読み込むjsonデータ
    private Jsondata jsondata;
    //オブジェクトの生成時に生成されるオブジェクトを指す変数
    private GameObject InstanceObject;
    //オブジェクトの生成時のy座標
    private float y;
    //オブジェクトの生成時に親オブジェクトを指す変数
    private GameObject Parent;
    //NowLoadingの時のcanvas、camera
    private GameObject canvas, UIcamera;

    //NavMeshのスクリプト
    NavMeshManager navscript;

    private void Start()
    {
        init();
        MapFolderName = "Sample2";
        MapGenerate(MapFolderName);
        navscript.BakeNavMesh();

        UISetActivefalse(canvas, UIcamera);
    }

    private void init()
    {
        y = 0;
        MapInfo info = new MapInfo();
        canvas = GameObject.Find("Canvas");
        UIcamera = GameObject.Find("UICamera");
        navscript = GameObject.Find("NavMeshmanager").GetComponent<NavMeshManager>();
    }

    public void MapGenerate(string MapFolderName)
    {
        //AssetBundle、GameObjectの宣言
        AssetBundle JsonAssetBundle, ObjectAssetBundle, ManagerAssetBundle, EnemyAssetBundle;
        GameObject Wall, Door, Floor, Item, Player, ExitArea, SceneManager, ItemManager;
        GameObject MoveZombie;

        //JsonファイルのAssetBundle読み込み、AssetBundleからファイルの読み込み
        JsonAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Map/" + MapFolderName);
        string mapdatainputString = JsonAssetBundle.LoadAsset<TextAsset>("Assets/MapData/" + MapFolderName + "/Mapdata.json").ToString();
        string mapinfoinputString = JsonAssetBundle.LoadAsset<TextAsset>("Assets/MapData/" + MapFolderName + "/MapInfo.json").ToString();

        //マップサイズ情報の読み込み
        MapInfo inputjson2 = JsonUtility.FromJson<MapInfo>(mapinfoinputString);

        //各座標のオブジェクト情報を代入するクラスの初期化
        jsondata = new Jsondata();
        mapSize = inputjson2.mapsize;
        jsondata.mapdata = new Mapdata[mapSize];

        //各座標のオブジェクト情報の代入
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(mapdatainputString);

        //Debug.Log(inputjson.mapdata[0].xcoor);

        //オブジェクトとマネージャーのAssetBundle読み込み、GameObjectとしての読み込み
        ObjectAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Objects");
        ManagerAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Manager");
        EnemyAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Enemies");

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Wall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        Player = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Player.prefab");
        ExitArea = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/ExitArea.prefab");
        SceneManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/SceneManager.prefab");
        ItemManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/ItemManager.prefab");
        MoveZombie = EnemyAssetBundle.LoadAsset<GameObject>("Assets/Resources/Enemy/MoveZombie.prefab");


        //オブジェクトをまとめるための親オブジェクトの読み込み
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
                        var exit = Instantiate(ExitArea, new Vector3(inputjson.mapdata[i].xcoor, 1f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        exit.name = ExitArea.name;
                        break;

                    case "Capture\\003.png":
                        InstanceObject = MoveZombie;
                        y = 1.5f;
                        Parent = null;
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

    private void UISetActivefalse(GameObject Can,GameObject Cam)
    {
        Can.SetActive(false);
        Cam.SetActive(false);
    }
}