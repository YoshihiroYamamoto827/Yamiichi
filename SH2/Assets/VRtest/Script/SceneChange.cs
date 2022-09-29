using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public void fadeInvoke()
    {
        Invoke("GameScene", 2);
    }

    void GameScene()
    {
        SceneManager.LoadScene("Scene1");
    }
}
