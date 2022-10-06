using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetBandle : MonoBehaviour
{
    AssetBundle Player;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Player = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ovrplayer");
        player = Player.LoadAsset<GameObject>("Assets/VRtest/Animator/ovrlayer.prefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
