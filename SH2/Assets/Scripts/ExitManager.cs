using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    GameObject scenemanager;
    sceneManager script;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("SceneManager");
        script = scenemanager.GetComponent<sceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            script.GameClear();
        }
    }
}
