using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

//MapGenerater
public class MapGanarater : MonoBehaviour
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
    //マップ生成時に生成されたオブジェクトを指す変数
    private GameObject Instanced;

    private void Start()
    {
        init();

    }

    private void init()
    {
        y = 0;
        MapInfo info = new MapInfo();
    }

    public void MapGenerate(string JsonFileName)
    {
        //アセットバンドルの宣言と初期化
        AssetBundle JsonAssetBundle=null, ObjectAssetBundle = null, ManagerAssetBundle = null, ParentAssetBundle = null;

        //GameObjectの宣言
        GameObject Wall, Door, Floor, Item, Player, SceneManager, ItemManager, WallParent, DoorParent, FloorParent, ItemParent;
        //JsonファイルのAssetBundleとAssetBundle内の対応するJsonファイルの読み込み
        if (JsonAssetBundle != null)JsonAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + MapFolderName);
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

        //オブジェクト、Magager、クローンをまとめる用の空のオブジェクトのAssetBundleの読み込み
        if (ObjectAssetBundle != null) ObjectAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/objects");
        if (ManagerAssetBundle != null) ManagerAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/manager");
        if (ParentAssetBundle != null) ParentAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/parents");

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Wall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Floor.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Item.prefab");
        Player = ObjectAssetBundle.LoadAsset<GameObject>("Assets/Resources/Objects/Player.prefab");
        SceneManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/SceneManager.prefab");
        ItemManager = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Manager/ItemManager.prefab");
        WallParent = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Parents/Wall.prefab");
        DoorParent = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Parents/Door.prefab");
        FloorParent = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Parents/Floor.prefab");
        ItemParent = ManagerAssetBundle.LoadAsset<GameObject>("Assets/Resources/Parents/Item.prefab");

        for (int i = 0; i < mapSize * mapSize; i++)
        {
            Instantiate(Floor, new Vector3(inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor), Quaternion.identity);

            if (inputjson.mapdata[i].objectname != null)
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
               Instanced = Instantiate(InstanceObject, new Vector3(inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor), Quaternion.identity);
                //if (Parent != null) Instanced.transform.parent = Parent.transform;
            }
        }

        Instantiate(SceneManager, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(ItemManager, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
}