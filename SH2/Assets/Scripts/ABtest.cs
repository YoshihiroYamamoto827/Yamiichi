using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ABtest : MonoBehaviour
{

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

    //読み込むjsonデータ
    private Jsondata jsondata;

    //マップの大きさ
    private int mapSize;

    GameObject InstanceObject;
    float y;
    bool FloorInstalled;

    int playerx,playery;

    // Start is called before the first frame update
    void Start()
    {
        AssetBundle Test1, Test2;
        GameObject cube1, cube2;
        var path1 = Application.streamingAssetsPath + "/testcube";
        var path2 = Application.streamingAssetsPath + "/testcube2";

        Test1 = AssetBundle.LoadFromFile(path1);
        Test2 = AssetBundle.LoadFromFile(path2);

        Debug.Log(Test1);

        cube1 = Test1.LoadAsset<GameObject>("TestCube");
        cube2 = Test2.LoadAsset<GameObject>("Assets/Resources/TestCube2.prefab");

        Instantiate(cube1, new Vector3(-1,1,0),Quaternion.identity);
        Instantiate(cube2, new Vector3(1, 1, 0), Quaternion.identity);

        //AssetBundle、GameObjectの宣言
        AssetBundle JsonAssetBundle, ObjectAssetBundle, ManagerAssetBundle, EnemyAssetBundle;
        GameObject Wall, EventWall, Door, Floor, Ceiling, Item, Player, ExitArea, SceneManager, ItemManager;
        GameObject MoveZombie;

        string MapFolderName = "sample6";

        JsonAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, MapFolderName));

        string mapdatainputString = JsonAssetBundle.LoadAsset<TextAsset>("Mapdata").ToString();
        string mapinfoinputString = JsonAssetBundle.LoadAsset<TextAsset>("MapInfo").ToString();

        //マップサイズ情報の読み込み
        MapInfo inputjson2 = JsonUtility.FromJson<MapInfo>(mapinfoinputString);

        //各座標のオブジェクト情報を代入するクラスの初期化
        jsondata = new Jsondata();
        mapSize = inputjson2.mapsize;
        jsondata.mapdata = new Mapdata[mapSize];

        //各座標のオブジェクト情報の代入
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(mapdatainputString);

        ObjectAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objects"));
        ManagerAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "manager"));
        EnemyAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "enemies"));

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Wall.prefab");
        EventWall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/EventWall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        Ceiling = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Ceiling.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        Player = GameObject.Find("OVRPlayerController");
        MoveZombie = EnemyAssetBundle.LoadAsset<GameObject>("Assets/Resources/Enemies/MoveZombie.prefab");

        for(int i = 0; i < mapSize * mapSize; i++)
        {
            FloorInstalled = false;
            if (inputjson.mapdata[i].objectname != "")
            {
                switch (inputjson.mapdata[i].objectname)
                {
                    case "Capture\\001.png":
                        InstanceObject = Wall;
                        y = 2f;
                        break;

                    case "Capture\\002.png":
                        InstanceObject = Door;
                        y = 1.5f;
                        break;

                    case "Capture\\003.png":
                        InstanceObject = MoveZombie;
                        y = 1.5f;
                        break;

                    case "Capture\\004.png":
                        InstanceObject = Wall;
                        y = 1f;
                        break;

                    case "Capture\\005.png":
                        InstanceObject = Item;
                        y = 1.5f;
                        break;

                    case "Capture\\006.png":
                        InstanceObject = Player;
                        playerx = inputjson.mapdata[i].xcoor;
                        playery = inputjson.mapdata[i].ycoor;
                        y = 1.5f;
                        break;

                    case "Capture\\007.png":
                        InstanceObject = EventWall;
                        y = 2f;
                        break;
                }
                if (InstanceObject != Player)
                {
                    ObjectInstance2(Floor, inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor, InstanceObject);
                    FloorInstalled = true;
                }
                }
            if (FloorInstalled == false)
            {
                var floor = Instantiate(Floor, new Vector3(inputjson.mapdata[i].xcoor, 0f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
                floor.name = Floor.name;

            }

            var ceiling = Instantiate(Ceiling, new Vector3(inputjson.mapdata[i].xcoor, 4f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
            ceiling.name = Ceiling.name;
        }

        Player.transform.Translate(playerx, 2f, playery);

    }

    private void ObjectInstance2(GameObject floorb, float x, float y, float z, GameObject objb)
    {
        var floora = Instantiate(floorb, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
        floora.name = floorb.name;

        if (objb != null)
        {
            var obja = Instantiate(objb, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            obja.name = objb.name;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
