using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    private GameObject itemManager;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            itemManager.GetComponent<ItemManager>().ItemTriggeradd();
            Destroy(this.gameObject);
        }
    }
}
