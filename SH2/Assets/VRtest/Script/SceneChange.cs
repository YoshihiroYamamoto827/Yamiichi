using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public GameObject TitlePanel;
    public GameObject HelpPanel;
    public GameObject MapSelectPanel;
    public GameObject EndPanel;

    public GameObject Pointer;
    public GameObject Button;
    public Text ButtonText;
    public GameObject PlayerController;
    public GameObject FlashLight;

    private void Start()
    {

        //表示するパネル変更
        TitlePanel.SetActive(false);
        HelpPanel.SetActive(false);
        MapSelectPanel.SetActive(false);
        EndPanel.SetActive(false);
        Button.SetActive(false);
        Pointer.SetActive(false);
        PlayerController.GetComponent<OVRPlayerController>().enabled = true;

        if (SceneManager.GetActiveScene().name != "Game")
        {
            Button.SetActive(true);
            Pointer.SetActive(true);
            PlayerController.GetComponent<OVRPlayerController>().enabled = false;
        }

        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitlePanel.SetActive(true);
            ButtonText.text = "Next";
        }
        else if (SceneManager.GetActiveScene().name == "Help")
        {
            HelpPanel.SetActive(true);
            ButtonText.text = "GameStart";
        }
        else if (SceneManager.GetActiveScene().name == "MapSelect")
        {
            MapSelectPanel.SetActive(true);
            ButtonText.text = "Title";
        }
        else if (SceneManager.GetActiveScene().name == "End")
        {
            EndPanel.SetActive(true);
            ButtonText.text = "Title";
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            GameObject obj = Instantiate(FlashLight, transform.position, Quaternion.identity);
        }

    }

    private void Update()
    {
        //マップ選択・タイトルへ割り込み
        if (OVRInput.Get(OVRInput.RawButton.A) &&
           OVRInput.Get(OVRInput.RawButton.B) &&
           OVRInput.Get(OVRInput.RawButton.RIndexTrigger) &&
           OVRInput.Get(OVRInput.RawButton.RHandTrigger) &&
           OVRInput.Get(OVRInput.RawButton.Start))
        {
            if (SceneManager.GetActiveScene().name == "Title")
                SceneManager.LoadScene("MapSelect");
            else if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "MapSelect")
            {
                SceneManager.LoadScene("Title");
            }
        }

        if (OVRInput.Get(OVRInput.RawButton.Y))
            SceneManager.LoadScene("End");
    }

    public void FadeInvoke()
    {
        if (SceneManager.GetActiveScene().name == "Help")
            Invoke("GameScene", 3);
        else if (SceneManager.GetActiveScene().name == "Title")
            Invoke("HelpScene", 3);
        else if (SceneManager.GetActiveScene().name == "End" || SceneManager.GetActiveScene().name == "MapSelect")
            Invoke("TitleScene", 3);
    }

    void GameScene()
    {
        SceneManager.LoadScene("Game");
        Button.SetActive(false);
        Pointer.SetActive(false);
    }

    void HelpScene()
    {
        SceneManager.LoadScene("Help");
    }

    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }


}