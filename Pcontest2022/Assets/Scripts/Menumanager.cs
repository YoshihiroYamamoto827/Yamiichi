using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menumanager : MonoBehaviour
{
    public GameObject menucanvas;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            player.SetActive(false);
            menucanvas.SetActive(true);
        }
    }

    public void MenuClose()
    {
        menucanvas.SetActive(false);
        player.SetActive(true);
    }
}
