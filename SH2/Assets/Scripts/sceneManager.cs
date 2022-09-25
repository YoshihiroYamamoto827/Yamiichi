using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject HelpPanel;
    public GameObject FadePanel;

    float alpha;
    public float alphaSpeed;
    Image fadeAlpha;

    private bool fadeout = false;
    private bool fadein = false;


    // Start is called before the first frame update
    void Start()
    {
        fadeAlpha = FadePanel.GetComponent<Image>();
        alpha = fadeAlpha.color.a;
        fadein = true;
    }

    private void Update()
    {
        if (fadeout == true)
            FadeOut();
        if (fadein == true)
            FadeIn();
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

    public void GametoStart()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void MapPreview()
    {
        SceneManager.LoadScene("MapPreviewScene");
    }

    public void HelpStart()
    {
        HelpPanel.SetActive(true);
        Invoke("MapPreview", 6);
        Invoke("FadeOut", 5);
    }

    //フェードイン・フェードアウト
    #region
    void FadeIn()
    {
        fadeAlpha.enabled = true;
        alpha -= alphaSpeed;
        fadeAlpha.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            fadein = false;
            fadeAlpha.enabled = false;
        }
    }

    void FadeOut()
    {
        fadeout = true;
        fadeAlpha.enabled = true;
        alpha += alphaSpeed;
        fadeAlpha.color = new Color(0, 0, 0, alpha);
        if(alpha>=1)
        {
            fadeout = false;
        }
    }
    #endregion
}
