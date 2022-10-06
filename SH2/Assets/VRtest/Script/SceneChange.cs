using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

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
    public Light lighting;

    [SerializeField] AudioClip[] clips;
    AudioSource source;

    float lightwaitTime = 0.0f;
    float footwaitTime = 0.0f;
    bool footSound;

    public GameObject l_controller;
    public GameObject r_controller;
    public GameObject l_hand;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name != "Game")
        {
            Button.SetActive(true);
            Pointer.SetActive(true);
            PlayerController.GetComponent<OVRPlayerController>().enabled = false;
            l_controller.SetActive(true);
            r_controller.SetActive(true);
            FlashLight.SetActive(false);
            l_hand.SetActive(false);         
        }

        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitlePanel.SetActive(true);
            ButtonText.text = "Next";
            source.PlayOneShot(clips[0]);
            source.loop = !source.loop;
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

        lightwaitTime += Time.deltaTime;
        footwaitTime += Time.deltaTime;

        if (lightwaitTime >= 0.5f)
        {
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                source.PlayOneShot(clips[4]);
                LightSwitch();
            }
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
        if (footwaitTime >= 0.912f)
            {
                Vector2 v = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
                if (v.x != 0 && v.y != 0)
                {
                    PlayFoot();
                }
            }
        }
    }

    //シーン変更
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


    void LightSwitch()
    {
        lightwaitTime = 0.0f;
        lighting.enabled = !lighting.enabled;

    }

    public void PlayFoot()
    {        
        footwaitTime = 0.0f;
        source.PlayOneShot(clips[Random.Range(1, 3)]);
    }
}