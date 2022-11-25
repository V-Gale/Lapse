using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent enemyAgent;
    public Transform player;
    FieldOfView fov;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;


    //States:
    public float attackRange = 1f;
    public bool playerInAttackRange;

    //Color:
    public Material enemyPatroling;
    public Material enemyChasing;
    public Material enemyAttacking;
    private Renderer newColor;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        fov = this.GetComponent<FieldOfView>();
        newColor = this.GetComponent<Renderer>();
        player = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!fov.playerInSight && !playerInAttackRange) Patroling(); 
        if (fov.playerInSight && !playerInAttackRange) Chasing();     
        if (fov.playerInSight && playerInAttackRange) Attacking();
    }
    void Patroling() 
    {      
        newColor.material = enemyPatroling;
        enemyAgent.SetDestination(this.GetComponent<EnemyPatrol>().target);
        this.GetComponent<EnemyPatrol>().EnemyPatroling();      
    }
    void Chasing()
    {
        newColor.material = enemyChasing;
        enemyAgent.speed = 6f;
        enemyAgent.SetDestination(player.position);
    }
    void Attacking()
    {      
        newColor.material = enemyAttacking;
        enemyAgent.speed = 6f;
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);
        anim.SetTrigger("Attacking");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
    private void OnAnimatorMove()
    {

    }
}
