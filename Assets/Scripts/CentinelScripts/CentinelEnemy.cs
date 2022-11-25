using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentinelEnemy : MonoBehaviour
{
    //Attacking variables
    public float timeBetweenAttacks = 0.5f;
    bool alreadyAttacked;
    public int amount = 1;

    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float attackRange;
    public bool playerInAttackRange;
    public bool playerCanBeAttacked;

    //Rendering materials
    public Renderer enemyRef1;
    public Renderer enemyRef2;
    public Material EnemyPatroling;
    public Material EnemyAttacking;

    //---------Other:
    public Light enemyLight;    
    public Transform spotTarget;
    Vector3 spherePos;
    public Animator anim;

    CentinelFOV fov;
    [SerializeField] ParticleSystem CentPS;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spotTarget = this.gameObject.transform.GetChild(0).transform;
        player = GameObject.Find("Player").transform;
        enemyRef1 = this.gameObject.transform.GetChild(1).GetComponent<Renderer>();
        enemyRef2 = this.gameObject.transform.GetChild(2).GetComponent<Renderer>();
        fov = this.GetComponent<CentinelFOV>();
        
        attackRange = enemyLight.range/4;
        spherePos = new Vector3(enemyLight.transform.position.x, enemyLight.transform.position.y - 3, enemyLight.transform.position.z);        
        playerCanBeAttacked = false;
        anim.SetBool("CanAttack", false);
    }
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(spherePos, attackRange, whatIsPlayer);

        if (!fov.playerInSight)
        {
            attackRange = enemyLight.range / 4;
            playerCanBeAttacked = false;
            anim.SetBool("CanAttack", false);
            if (!playerCanBeAttacked)
            {
                enemyLight.transform.LookAt(spotTarget);               
                Patroling();
            }
        }
        if (playerInAttackRange && fov.playerInSight)
        {
            attackRange = fov.radius;
            playerCanBeAttacked = true;
            if (playerCanBeAttacked)
            {                
                enemyLight.transform.LookAt(player);
                anim.SetBool("CanAttack", true);
                Attacking();
            }
        }
    }


    private void Patroling()
    {       
        CentPS.Stop();
        enemyLight.color = Color.yellow;
        enemyRef1.material = EnemyPatroling;
        enemyRef2.material = EnemyPatroling;
    }
    private void Attacking()
    {
        enemyLight.color = Color.red;
        enemyRef1.material = EnemyAttacking;
        enemyRef2.material = EnemyAttacking;
        
        if (!alreadyAttacked)
        {
            player.GetComponent<HAD>().SubstractLife(amount);
            CentinelParticle();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void CentinelParticle() 
    {
        CentPS.Play();
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, attackRange);
    }
}

