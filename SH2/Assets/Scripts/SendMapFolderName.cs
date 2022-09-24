using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SendMapFolderName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
