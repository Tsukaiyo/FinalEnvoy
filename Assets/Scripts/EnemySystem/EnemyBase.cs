using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyBase : MonoBehaviour
{
    //movement
    private float walkSpeed = 1.5f;
    private float runSpeed = 4.5f;
    private float currentMoveSpeed;
    private bool isGrounded;

    //behaviour
    private bool isAttacking = false;

    //ranges
    private float sightRange = 7.5f;
    private float attackRange = 1f;
    private bool inSightRange;
    private bool inAttackRange;

    //references
    private NavMeshAgent agent;
    private PlayerController playerRef;
    public Transform groundCheckRef;
    public LayerMask groundLayer, playerLayer;
    private Transform currentTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSurroundings();

        //basic behaviour
        if (!inSightRange && !inAttackRange) Patrol();
        if (!inSightRange && inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange && !isAttacking) AttackPlayer();

        agent.speed = currentMoveSpeed;
        agent.SetDestination(currentTarget.position);
    }

    //Checks whether the enemy can see and/or attack the player
    void CheckSurroundings()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        isGrounded = Physics.CheckSphere(groundCheckRef.position, 0.2f, groundLayer);
    }

    //patrol area for player
    void Patrol()
    {
        currentMoveSpeed = walkSpeed;
        //set the currentTarget to a patrol point
    }

    //chase the player before attacking
    void ChasePlayer()
    {
        currentMoveSpeed = runSpeed;
        currentTarget = playerRef.transform;
    }

    //attack the player
    void AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true;
        transform.LookAt(playerRef.transform);

        //replace comment with attack function

        agent.isStopped = false;
        isAttacking = false;
    }
}
