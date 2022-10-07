using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetBandle : MonoBehaviour
{
    AssetBundle Player;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Player = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/OVRPlayer");
        player = Player.LoadAsset<GameObject>("Assets/VRtest/Scenes/OVRPlayer.prefab");
        Debug.Log(player.name);
        player = Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        player.name = Player.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
