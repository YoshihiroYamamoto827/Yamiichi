using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockWindow : MonoBehaviour
{
    private GameObject windowzombie, window;
    private Animator anim;

    public AudioClip Knockwindow;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        windowzombie = GameObject.Find("Zombie2");
        anim = windowzombie.GetComponent<Animator>();
        window = GameObject.Find("Window");
        audioSource = window.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Player")
        {
            audioSource.PlayOneShot(Knockwindow);
            anim.SetTrigger("Attack");
        }
        
    }

        
}
