using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private Animator MZanim;

    [SerializeField]
    [Tooltip("追いかける対象")]
    private GameObject MEplayer;

    private NavMeshAgent agent;

    int attackInterval = 1;
    bool attacking = false;
    public bool moveEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (MZanim != null)
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
            MZanim.SetTrigger("Attack");
            yield return new WaitForSeconds(attackInterval);
            attacking = false;
            moveEnabled = true;
        }
        yield return null;
    }

    void Move()
    {
        
            MZanim.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);

            // プレイヤーを目指して進む
            //agent.destination = MEplayer.transform.position;
        
    }

    void Stop()
    {
            agent.speed = 0;
            MZanim.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
    }

    public void init()
    {
        MZanim = gameObject.GetComponent<Animator>();

        MEplayer = GameObject.Find("Player");

        //NavMeshAgentを保持しておく
        agent = GetComponent<NavMeshAgent>();
    }
}
