using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KamikazeMovement : MonoBehaviour
{
    //Checking FOV:
    public NavMeshAgent enemyAgent;
    public Transform player;
    FieldOfView fov;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    //States:
    public float attackRange = 1f;
    public bool playerInAttackRange;
    public Animator anim;

    //Color:
    public Material enemyPatroling;
    public Material enemyChasing;
    public Material enemyAttacking;

    public Material KamHeadPatroling;
    public Material KamHeadChasing;
    public Material KamHeadAttacking;

    public Material KamHeadExploding;
    public Material enemyExploding;
    
    private Renderer newColor;
    private Renderer KamHead;

    //Attacking
    public int amount = 20;
    ParticleSystem KamPE;
    private void Awake()
    {
        fov = this.GetComponent<FieldOfView>();
        newColor = this.GetComponent<Renderer>();
        player = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        KamHead = this.gameObject.transform.GetChild(1).GetComponent<Renderer>();
        KamPE = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!fov.playerInSight && !playerInAttackRange) Patroling();
        if (fov.playerInSight && !playerInAttackRange) Chasing();
        if (fov.playerInSight && playerInAttackRange) Attacking();
        
    }

    private void Patroling() 
    {
        newColor.material = enemyPatroling;
        KamHead.material = KamHeadPatroling;
        enemyAgent.SetDestination(this.GetComponent<EnemyPatrol>().target);
        this.GetComponent<EnemyPatrol>().EnemyPatroling();
    }
    private void Chasing() 
    {
        newColor.material = enemyChasing;
        KamHead.material = KamHeadChasing;
        enemyAgent.speed = 7f;
        enemyAgent.SetDestination(player.position);
    }

    private void Attacking() 
    {
        newColor.material = enemyAttacking;
        KamHead.material = KamHeadAttacking;
        enemyAgent.SetDestination(transform.position);
        anim.SetTrigger("Attacking");
        transform.LookAt(player);
        
        StartCoroutine(Explode());       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    IEnumerator Explode() 
    {

        newColor.material = enemyExploding;
        KamHead.material = KamHeadExploding;
        yield return new WaitForSeconds(1f);
        player.GetComponent<HAD>().SubstractLife(amount);
        
        Instantiate(KamPE, transform.position, transform.rotation).Play();
        FindObjectOfType<AudioManager>().Play("KamikazeExplosion");
        Destroy(KamPE);
        Destroy(gameObject);
    }

    private void OnAnimatorMove()
    {
        
    }
}
