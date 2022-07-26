using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    private float raydistance;

    // Start is called before the first frame update
    void Start()
    {
        raydistance = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = transform.forward;

        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * raydistance, Color.red);

    }
}
