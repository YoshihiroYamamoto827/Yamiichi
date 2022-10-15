using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemGet2 : MonoBehaviour
{
    int ItemCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemCount++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("ExitDoor") && ItemCount >= 3)
        {
            SceneManager.LoadScene("EndScene");
            Debug.Log("endScene");
        }
    }
}