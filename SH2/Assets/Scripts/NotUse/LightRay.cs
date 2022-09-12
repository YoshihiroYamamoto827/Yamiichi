using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    private float raydistance;
    public GameObject face;
    int i = 1;

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

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.gameObject.CompareTag("Respawn")&&i>=1)
            {
                Debug.Log(hit.collider.gameObject.transform.position);
                Instantiate(face, hit.collider.gameObject.transform.position, Quaternion.identity);
                i--;
            }
            
        }

    }
}
