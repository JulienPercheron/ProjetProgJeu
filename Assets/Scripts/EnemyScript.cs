using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{
    NavMeshAgent agent;
    GameObject player;
    public float detectionRadius = 8.0f; 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && Vector3.Distance(this.transform.position, player.transform.position) < detectionRadius)
        {
            agent.destination = player.transform.position;
        }

        animator.SetFloat("Velocity", agent.velocity.normalized.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            this.PointsDeVie -= 1;
            if (this.PointsDeVie <= 0)
                Destroy(this.gameObject);
        }
    }
}
