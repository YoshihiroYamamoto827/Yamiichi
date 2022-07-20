using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
     private int ItemTrigger;
     private bool MessageTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        ItemTrigger = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MessageTrigger)
        {
            if (ItemTrigger == 3)
            {
                Debug.Log("Open");
                MessageTrigger = false;
            }
        }
    }
        

    public void ItemTriggeradd()
    {
        ItemTrigger++;
    }
}
