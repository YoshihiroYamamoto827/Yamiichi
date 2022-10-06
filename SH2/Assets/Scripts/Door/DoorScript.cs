
using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    //　ドアエリアに入っているかどうか
    private bool isNear;
    //　ドアのアニメーター
    private Animator animator;

    void Start()
    {
        isNear = false;
        animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isNear)
        {
            animator.SetBool("Open", !animator.GetBool("Open"));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isNear = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            isNear = false;
        }
    }
}
