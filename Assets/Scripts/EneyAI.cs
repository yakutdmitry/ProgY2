using System;
using System.Runtime.InteropServices;
using FSM.Scripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EneyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public LayerMask whatIsPlayer;
    public FsmController fsmController;
    private Animator animator;
    
    public Vector3 walkPoint;
    private bool pointSet ;
    public float pointRange;
    
    public float cooldown;
    private bool attacked;
    public float baseSpeed = 3.5f;
    
    public float sightDistance, attackDistance;
    public bool playerInSight, playerInAttack;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightDistance, whatIsPlayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackDistance, whatIsPlayer);

        if (!fsmController.Ulting)
        {
            if (!playerInSight && !playerInAttack)
            {
                animator.SetTrigger("Walking");
                Patrolling();
            }

            if (playerInSight && !playerInAttack)
            {
                animator.SetTrigger("Running");
                Chase();
            }

            if (playerInAttack)
            {
                Attack();
            }
            animator.SetBool("Ulting", false);
        }
        
        if (fsmController.Ulting)
        {
            animator.SetBool("Ulting", true);
            RunAway();
            Debug.Log("ULTIIIIIIIIIIIINGGGGGGGGGG");
            
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
            agent.speed = baseSpeed;
            // animator.SetTrigger("Walking");
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
        agent.speed += 2;
        // animator.SetTrigger("Running");
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

    private void RunAway()
    {
        // animator.SetTrigger("RunAway");
        agent.speed = 0;
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
