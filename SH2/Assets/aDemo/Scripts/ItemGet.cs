using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemGet : MonoBehaviour
{
    int ItemCount = 0;
    AudioSource item;

    private void Start()
    {
        item = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            ItemCount++;
            Destroy(other.gameObject);
            item.Play();
        }
        if (other.gameObject.CompareTag("ExitDoor") && ItemCount >= 3)
        {
            SceneManager.LoadScene("End");
            Debug.Log("endScene");
        }

    }
}
