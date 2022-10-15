using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    AudioSource voice;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Player"||other.name=="OVRPlayerController")
        {
            var direction = other.transform.position - transform.position;
            direction.y = 0;

            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

            voice = GetComponent<AudioSource>();
            voice.Play();
        }
    }
}
