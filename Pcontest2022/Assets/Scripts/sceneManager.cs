using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StarttoGame()
    {
        SceneManager.LoadScene("HouseScene");
    }

    public void StarttoTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void TutorialtoStart()
    {
        tutorialPanel.SetActive(false);
    }
}
