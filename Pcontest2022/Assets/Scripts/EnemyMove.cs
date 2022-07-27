using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    [Tooltip("追いかける対象")]
    private GameObject player;

    private NavMeshAgent agent;

    int attackInterval = 1;
    bool attacking = false;
    public bool moveEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        // NavMeshAgentを保持しておく
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveEnabled)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(AttackTimer());
        }
    }

    IEnumerator AttackTimer()
    {
        if (!attacking)
        {
            attacking = true;
            moveEnabled = false;
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(attackInterval);
            attacking = false;
            moveEnabled = true;
        }
        yield return null;
    }

    void Move()
    {
        anim.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);

        // プレイヤーを目指して進む
        agent.destination = player.transform.position;
    }

    void Stop()
    {
        agent.speed = 0;
        anim.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
    }
}
