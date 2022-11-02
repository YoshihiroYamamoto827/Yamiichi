using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public float rayDistance;
    public string objname;

    // Update is called once per frame
    public void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            objname = hit.collider.gameObject.name;
        }
    }
}

