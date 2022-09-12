using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    GameObject Player;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), Speed * Time.deltaTime);
        transform.LookAt(Player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
