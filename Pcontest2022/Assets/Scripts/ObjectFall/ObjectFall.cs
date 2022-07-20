using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private GameObject cadaver, cadavererea;
    private Vector3 cadaverfall;
    private bool Falltrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        cadaver = (GameObject)Resources.Load("Cadaver");
        cadavererea = GameObject.Find("CadaverErea");
        cadaverfall = new Vector3(cadavererea.transform.position.x, cadavererea.transform.position.y + 3.0f, cadavererea.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Falltrigger)
        {
            if (other.gameObject.name == "Player")
                Fallcadaver();
            Falltrigger = false;
        }
         
    }

    private void Fallcadaver()
    {
        Instantiate(cadaver,cadaverfall,Quaternion.identity);
    }
}
