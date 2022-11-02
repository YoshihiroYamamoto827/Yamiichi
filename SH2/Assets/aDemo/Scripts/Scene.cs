using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject helpPanel;
    public GameObject developerPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Help()
    {
        
        helpPanel.SetActive(true);mainUI.SetActive(false);
    }
    public void MapSelect()
    {
        
        developerPanel.SetActive(true);mainUI.SetActive(false);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
