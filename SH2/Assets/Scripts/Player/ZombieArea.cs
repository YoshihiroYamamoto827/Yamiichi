using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArea : MonoBehaviour
{

    public bool isArea;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MoveCollider")
        {
            isArea = true;
        }

        if(other.gameObject.name == "MoveZombie")
        {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MoveCollider")
        {
            isArea = false;
        }
    }
}
