using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	#region Public Members
    public float patrolSpeed = 2f;
    public float chaseSpeed = 0.5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;
	#endregion
	
	#region Private Members
    private EnemySight enemySight;
    private UnityEngine.AI.NavMeshAgent nav;
    private Transform player;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;
	#endregion

    // Use this for initialization
    void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player")!=null)
        { player = GameObject.FindGameObjectWithTag("Player").transform; }
        else player = GameObject.FindGameObjectWithTag("Dog").transform;
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySight.personalLastSighting != enemySight.resetPosition )
			Chasing();
        else 
			Patrolling();
    }

    void Shooting()
    {
        nav.Stop();
    }

    void Chasing()
    {
       
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4f)
            nav.destination = enemySight.personalLastSighting;
      nav.speed = 4f;
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseWaitTime)
            {
                enemySight.personalLastSighting = enemySight.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;
        nav.destination = patrolWayPoints[wayPointIndex].position;
        if (nav.destination == enemySight.resetPosition || nav.remainingDistance == 0)
        {

            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                {

                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
        }
        else
        {
            patrolTimer = 0;
        }
    }
}
