using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    GameObject scenemanager,itemmanager;
    sceneManager script;
    Itemmanager script2;

    // Start is called before the first frame update
    void Start()
    {
        scenemanager = GameObject.Find("SceneManager");
        script = scenemanager.GetComponent<sceneManager>();
        itemmanager = GameObject.Find("ItemManager");
        script2 = itemmanager.GetComponent<Itemmanager>();
        script2.LoadExitArea();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            GameClear();
        }
    }

    private void GameClear()
    {
        SceneManager.LoadScene("EndScene");
    }
}
