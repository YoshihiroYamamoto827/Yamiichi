using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public int fadetime;

    public void FadeInvoke()
    {
        if (SceneManager.GetActiveScene().name=="Help")
            Invoke("GameScene", fadetime);
        else if(SceneManager.GetActiveScene().name=="Title")
            Invoke("HelpScene", fadetime);
    }

    /*public void Help()
    {
        invoke("GameScene", 8);
    }*/

    void GameScene()
    {
        SceneManager.LoadScene("Scene1");
    }

    void HelpScene()
    {
        SceneManager.LoadScene("Help");
    }

}
