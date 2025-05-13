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
    public float baseSpeed;
    
    public float sightDistance, attackDistance;
    public bool playerInSight, playerInAttack;

    public GameData _gameData;
    
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        DataManager.loadData(_gameData, "Data.json");
        pointRange = _gameData.enemyPointRange;
        cooldown = _gameData.enemyCooldown;
        baseSpeed = _gameData.enemyBaseSpeed;
        sightDistance= _gameData.enemySightDistance;
        attackDistance = _gameData.enemyAttackDistance;
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
        
    }

    private void Attack()
    {
        
        agent.SetDestination(transform.position);
        
        transform.LookAt(target);

        if (!attacked)
        {
            
            attacked = true;
            Invoke(nameof(resetAttack), cooldown);
        }
    }

    private void RunAway()
    {
        agent.speed = 0;
    }

    private void resetAttack()
    {
        _gameData.attacksPerformed++;
        attacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-pointRange, pointRange);
        float randomX = Random.Range(-pointRange, pointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        pointSet = true;
        _gameData.pointsSet++;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && fsmController.Ulting)
        {
            Debug.Log("COLLISION WIH PLAYER");
            _gameData.enemiesKilled++;
            Destroy(gameObject);
            
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        DataManager.saveData(_gameData, "Data.json");
    }

    private void OnApplicationQuit()
    {
        DataManager.saveData(_gameData, "Data.json");
    }
}
