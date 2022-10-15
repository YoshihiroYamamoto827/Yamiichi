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

    //�ǂݍ���json�f�[�^
    private Jsondata jsondata;

    //�}�b�v�̑傫��
    private int mapSize;

    GameObject InstanceObject;
    float y;
    bool FloorInstalled;

    int playerx,playery;
    private Vector3 rotate = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

        //AssetBundle�AGameObject�̐錾
        AssetBundle JsonAssetBundle, ObjectAssetBundle, ManagerAssetBundle, EnemyAssetBundle;
        GameObject Wall, EventWall, Door, Floor, Ceiling, Item, Player, ExitArea, SceneManager, ItemManager;
        GameObject MoveZombie;

        string MapFolderName = "manual";

        JsonAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, MapFolderName));

        string mapdatainputString = JsonAssetBundle.LoadAsset<TextAsset>("Mapdata").ToString();
        string mapinfoinputString = JsonAssetBundle.LoadAsset<TextAsset>("MapInfo").ToString();

        //�}�b�v�T�C�Y���̓ǂݍ���
        MapInfo inputjson2 = JsonUtility.FromJson<MapInfo>(mapinfoinputString);

        //�e���W�̃I�u�W�F�N�g����������N���X�̏�����
        jsondata = new Jsondata();
        mapSize = inputjson2.mapsize;
        jsondata.mapdata = new Mapdata[mapSize];

        //�e���W�̃I�u�W�F�N�g���̑��
        Jsondata inputjson = JsonUtility.FromJson<Jsondata>(mapdatainputString);

        ObjectAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objects"));
        ManagerAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "manager"));
        EnemyAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "enemies"));

        Wall = ObjectAssetBundle.LoadAsset<GameObject>("Wall.prefab");
        EventWall = ObjectAssetBundle.LoadAsset<GameObject>("EventWall.prefab");
        Door = ObjectAssetBundle.LoadAsset<GameObject>("Door.prefab");
        Floor = ObjectAssetBundle.LoadAsset<GameObject>("Floor.prefab");
        Ceiling = ObjectAssetBundle.LoadAsset<GameObject>("Ceiling.prefab");
        Item = ObjectAssetBundle.LoadAsset<GameObject>("Item.prefab");
        Player = GameObject.Find("Player");
        MoveZombie = EnemyAssetBundle.LoadAsset<GameObject>("MoveZombie.prefab");
        ExitArea = ObjectAssetBundle.LoadAsset<GameObject>("ExitArea.prefab");
        ItemManager = ManagerAssetBundle.LoadAsset<GameObject>("ItemManager.prefab");

        Instantiate(ItemManager);

        for (int i = 0; i < mapSize * mapSize; i++)
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
                        var exit = Instantiate(ExitArea, new Vector3(inputjson.mapdata[i].xcoor, 1f, inputjson.mapdata[i].ycoor), Quaternion.identity);
                        exit.name = ExitArea.name;
                        rotate.y = 90f;
                        break;

                    case "Capture\\003.png":
                        InstanceObject = MoveZombie;
                        y = 1.5f;
                        rotate.y = 90f;
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
                        rotate.y = 180f;
                        break;

                    case "Capture\\007.png":
                        InstanceObject = EventWall;
                        y = 2f;
                        break;
                }
                if (InstanceObject != Player)
                {
                    ObjectInstance2(Floor, mapSize - inputjson.mapdata[i].xcoor, y, inputjson.mapdata[i].ycoor, InstanceObject, rotate);
                    FloorInstalled = true;
                }
                }
            if (FloorInstalled == false)
            {
                var floor = Instantiate(Floor, new Vector3(mapSize - inputjson.mapdata[i].xcoor, 0f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
                floor.name = Floor.name;

            }

            var ceiling = Instantiate(Ceiling, new Vector3(mapSize - inputjson.mapdata[i].xcoor, 4f, inputjson.mapdata[i].ycoor), Quaternion.identity) as GameObject;
            ceiling.name = Ceiling.name;
        }

        Player.transform.Translate(playerx, 2f, playery);
    }

    private void ObjectInstance2(GameObject floorb, float x, float y, float z, GameObject objb, Vector3 Rotate)
    {
        var floora = Instantiate(floorb, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
        floora.name = floorb.name;

        if (objb != null)
        {
            var obja = Instantiate(objb, new Vector3(x, y, z), Quaternion.Euler(Rotate)) as GameObject;
            obja.name = objb.name;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
