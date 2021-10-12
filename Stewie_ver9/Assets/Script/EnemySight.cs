using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
	#region Public Members
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
	#endregion
	
	#region Private Members
    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private GameObject player;
    private Animator playerAnim;
    private Tags hash;
	#endregion


    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
       
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<Tags>();

        personalLastSighting = resetPosition;
    }

    // Update is called once per frame
    void Update()
    {if (GameObject.FindGameObjectWithTag("Dog")!=null)
        {
            player = GameObject.FindGameObjectWithTag("Dog");
            Debug.Log(player.name);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                    }
                }
            }
            personalLastSighting = player.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        { playerInSight = false; }
    }
    float CalculatePathLength(Vector3 targetPosition)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;


        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        float pathLength = 0;
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
