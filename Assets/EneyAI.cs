using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EneyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public Transform target;

    public LayerMask whatIsGround, whatIsPlayer;
    
    public Vector3 walkPoint;
    private bool pointSet ;
    public float pointRange;
    
    public float cooldown;
    private bool attacked;
    
    public float sightDistance, attackDistance;
    public bool playerInSight, playerInAttack;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightDistance, whatIsPlayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackDistance, whatIsPlayer);

        if (!playerInSight && !playerInAttack)
        {
            Patrolling();
        }

        if (playerInSight && !playerInAttack)
        {
            Chase();
        }

        if (playerInAttack)
        {
            Attack();
        }
    }

    private void Patrolling()
    {
        if (!pointSet)
        {
            SearchWalkPoint();
            Debug.Log("No point");
        }

        if (pointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.Log("point");
        }
        
        Vector3 distToWalkPoint = transform.position - walkPoint;
        if (distToWalkPoint.magnitude < 1f)
        {
            pointSet = false;
        }
    }

    private void Chase()
    {
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        
        transform.LookAt(target);

        if (!attacked)
        {
            ///put animation here 
            
            attacked = true;
            Invoke(nameof(resetAttack), cooldown);
        }
    }

    private void resetAttack()
    {
        attacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-pointRange, pointRange);
        float randomX = Random.Range(-pointRange, pointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        pointSet = true;
        
        // if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
        // {
        //     pointSet = true;
        // }
        // else
        // {
        //     Debug.logger.Log("Raycast Problem");
        // }
    }
}
