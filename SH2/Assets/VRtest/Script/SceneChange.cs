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

    private void Start()
    {

        //表示するパネル変更
        TitlePanel.SetActive(false);
        HelpPanel.SetActive(false);
        MapSelectPanel.SetActive(false);
        EndPanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitlePanel.SetActive(true);
            ButtonText.text = "Next";
        }
        if (SceneManager.GetActiveScene().name == "Help")
        {
            HelpPanel.SetActive(true);
            ButtonText.text = "GameStart";
        }
        if (SceneManager.GetActiveScene().name == "MapSelect")
        {
            MapSelectPanel.SetActive(true);
            ButtonText.text = "Title";
        }
        if (SceneManager.GetActiveScene().name == "End")
        {
            EndPanel.SetActive(true);
            ButtonText.text = "Title";
        }
        if (SceneManager.GetActiveScene().name != "Game")
        {
            Button.SetActive(true);
            Pointer.SetActive(true);
        }

        /*else if (SceneManager.GetActiveScene().name == "Help")
            HelpPanel.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "MapSelect")
            HelpPanel.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "End")
            EndPanel.SetActive(true);*/
    }

    private void Update()
    {
        //マップ選択・タイトルへ割り込み
        if(OVRInput.Get(OVRInput.RawButton.A)&&
           OVRInput.Get(OVRInput.RawButton.B)&&
           OVRInput.Get(OVRInput.RawButton.RIndexTrigger)&&
           OVRInput.Get(OVRInput.RawButton.RHandTrigger)&&
           OVRInput.Get(OVRInput.RawButton.Start))
        {
            if (SceneManager.GetActiveScene().name == "Title")
                SceneManager.LoadScene("MapSelect");
            else
                SceneManager.LoadScene("Title");
        }
    }

    public void FadeInvoke()
    {
        if (SceneManager.GetActiveScene().name=="Help")
            Invoke("GameScene", 3);
        else if(SceneManager.GetActiveScene().name=="Title")
            Invoke("HelpScene", 3);
        else if (SceneManager.GetActiveScene().name == "End")
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
