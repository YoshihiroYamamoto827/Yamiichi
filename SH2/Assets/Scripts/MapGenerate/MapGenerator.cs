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

    struct MoveEnemyInfo
    {
        public int[] x;
        public int[] y;
        public GameObject[] EnemyType;
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
    //敵の数をカウントする変数
    private int EnemyCount;

    //NavMeshのスクリプト
    NavMeshManager navscript;
    //EnemyMoveのスクリプト
    EnemyMove[] enemymovescript;

    private void Start()
    {
        init();
        MapGenerate(MapFolderName);
        //UISetActivefalse(canvas, UIcamera);
    }

    private void init()
    {
        MapFolderName = "Sample6";
        Debug.Log(MapFolderName);
        y = 0;
        EnemyCount = 0;
        MapInfo info = new MapInfo();
        //canvas = GameObject.Find("Canvas");
        //UIcamera = GameObject.Find("UICamera");
        navscript = GameObject.Find("NavMeshmanager").GetComponent<NavMeshManager>();
    }

    public void MapGenerate(string MapFolderName)
    {
        //AssetBundle、GameObjectの宣言
        AssetBundle JsonAssetBundle, ObjectAssetBundle, ManagerAssetBundle, EnemyAssetBundle;
        GameObject Wall, EventWall, Door, Floor, Ceiling, Item, Player, ExitArea, SceneManager, ItemManager;
        GameObject MoveZombie;

        //敵の生成時にスクリプトを指定するために、GameObjectを格納する変数
        GameObject[] ene;

        /*void AssetBundleUnload()
        {
            JsonAssetBundle.Unload(false);
            ObjectAssetBundle.Unload(false);
            ManagerAssetBundle.Unload(false);
            EnemyAssetBundle.Unload(false);
        }

        AssetBundleUnload();*/
        
        //JsonファイルのAssetBundle読み込み、AssetBundleからファイルの読み込み
        JsonAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, MapFolderName));
        Debug.Log(MapFolderName);
        string mapdatainputString = JsonAssetBundle.LoadAsset<TextAsset>("Mapdata").ToString();
        string mapinfoinputString = JsonAssetBundle.LoadAsset<TextAsset>("MapInfo").ToString();
        Debug.Log(mapdatainputString);
        Debug.Log(mapinfoinputString);


        //マップサイズ情報の読み込み
        MapInfo inputjson2 = JsonUtility.FromJson<MapInfo>(mapinfoinputString);

        //各座標のオブジェクト情報を代入するクラスの初期化
        jsondata = new Jsondata();
        mapSize = inputjson2.mapsize;
        jsondata.mapdata = new Mapdata[mapSize];
        MoveEnemyInfo MEInfo;
        MEInfo.x = new int[mapSize * mapSize];
        MEInfo.y = new int[mapSize * mapSize];
        MEInfo.EnemyType = new GameObject[mapSize * mapSize];
        

        //各座標のオブジェクト情報の代入
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(mapdatainputString);

        //Debug.Log(inputjson.mapdata[0].xcoor);

        //オブジェクトとマネージャーのAssetBundle読み込み、GameObjectとしての読み込み
        ObjectAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objects"));
        ManagerAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "manager"));
        EnemyAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "enemies"));

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Wall.prefab");
        EventWall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/EventWall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        Ceiling = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Ceiling.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        Player = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/OVRPlayer.prefab");
        ExitArea = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/ExitArea.prefab");
        SceneManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/SceneManager.prefab");
        ItemManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/ItemManager.prefab");
        MoveZombie = EnemyAssetBundle.LoadAsset<GameObject>("Assets/Resources/Enemies/MoveZombie.prefab");


        //オブジェクトをまとめるための親オブジェクトの読み込み
        GameObject WallParent = new GameObject("Walls");
        GameObject DoorParent = new GameObject("Doors");
        GameObject FloorParent = new GameObject("Floors");
        GameObject CeilingParent = new GameObject("Ceilings");
        GameObject ItemParent = new GameObject("Items");
        GameObject MoveEnemy = new GameObject("MoveEnemy");


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
                        InstanceObject = null;
                        y = 1.5f;
                        Parent = null;
                        MEInfo.x[EnemyCount] = inputjson.mapdata[i].xcoor;
                        MEInfo.y[EnemyCount] = inputjson.mapdata[i].ycoor;
                        MEInfo.EnemyType[EnemyCount] = MoveZombie;
                        EnemyCount++;
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

                        case "Capture\\007.png":
                        InstanceObject = EventWall;
                        y = 2f;
                        Parent = WallParent;
                        break;
                }
                ObjectInstance2(Floor, FloorParent, inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor, InstanceObject, Parent);
            }
            var floor = Instantiate(Floor, new Vector3(inputjson.mapdata[i].xcoor, 0f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
            floor.name = Floor.name;
            if (FloorParent != null) floor.transform.parent = FloorParent.transform;

            var ceiling = Instantiate(Ceiling, new Vector3(inputjson.mapdata[i].xcoor, 4f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
            ceiling.name = Ceiling.name;
            if (CeilingParent != null) ceiling.transform.parent = CeilingParent.transform;

        }
        navscript.BakeNavMesh();

        for(int j = 0; j < EnemyCount; j++)
        {
            EnemyInstance(MEInfo.x[j], MEInfo.y[j], MEInfo.EnemyType[j], MoveEnemy, j);
        }

        enemymovescript = new EnemyMove[EnemyCount];
        ene = new GameObject[EnemyCount];

        ene = GameObject.FindGameObjectsWithTag("MoveEnemy");
        
        for (int k = 0; k < EnemyCount; k++)
        {
            Debug.Log(ene[k].name);
            enemymovescript[k] = ene[k].GetComponent<EnemyMove>();
            enemymovescript[k].init();
        }

    }

    private void ObjectInstance1(GameObject objb)
    {
        var obja = Instantiate(objb, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        obja.name = objb.name;
    }

    private void ObjectInstance2(GameObject floorb, GameObject floorparent, float x, float y, float z, GameObject objb, GameObject parent)
    {
        var floora = Instantiate(floorb, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
        floora.name = floorb.name;
        if (floorparent != null) floora.transform.parent = floorparent.transform;

        if (objb != null)
        {
            var obja = Instantiate(objb, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            obja.name = objb.name;
            if (parent != null) obja.transform.parent = parent.transform;
        }
        
    }

    private void EnemyInstance(int x, int y, GameObject eneb, GameObject parent, int j)
    {
        var enea = Instantiate(eneb, new Vector3(x, 1.5f, y), Quaternion.identity) as GameObject;
        enea.name = eneb.name + j.ToString();
        if (parent != null) enea.transform.parent = parent.transform;
    }

    /*private void UISetActivefalse(GameObject Can, GameObject Cam)
    {
        Can.SetActive(false);
        Cam.SetActive(false);
    }*/
}