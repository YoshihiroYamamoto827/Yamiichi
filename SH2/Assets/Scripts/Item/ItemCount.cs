using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCount : MonoBehaviour
{
    GameObject itemManager;
    Itemmanager script;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManager");
        script = itemManager.GetComponent<Itemmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CustomHandLeft")
        {
            script.itemcounterAdd();
            Destroy(this.gameObject);
        }
    }


}
