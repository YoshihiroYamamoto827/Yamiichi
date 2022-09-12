using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemmanager : MonoBehaviour
{
    public int itemcounter = 0;
    public GameObject exitarea;

    // Start is called before the first frame update
    void Start()
    {
        exitarea = GameObject.Find("ExitArea");
        exitarea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(itemcounter >= 3)
        {
            Debug.Log("Area Opened");
            exitarea.SetActive(true);
        }
    }

    public void itemcounterAdd()
    {
        itemcounter++;
    }
}
