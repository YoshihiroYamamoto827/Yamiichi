using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveData : ScriptableObject
{
    public GameObject player = GameObject.Find("Player");
    int attackInterval = 1;
    bool attacking = false;
    public bool moveEnabled = true;
}
