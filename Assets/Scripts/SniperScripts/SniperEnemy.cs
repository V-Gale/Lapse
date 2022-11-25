using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    public Transform player;
    public Transform target;
    FieldOfView fov;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    //Bullet
    public Transform bulletSpawn;
    public GameObject bullet;
    public float bulletSpeed = 1f;

    //Attacking
    bool alreadyAttacked;
    float timeBetweenAttacks = 0.2f;

    //Animation
    Animator anim;

    //Rendering Materials
    public Material SniperAttacking;
    public Material SniperPatroling;
    Renderer SniperRend;
    void Awake()
    {
        fov = this.GetComponent<FieldOfView>();
        player = GameObject.Find("Player").transform;
        SniperRend = this.GetComponent<Renderer>();
        anim = this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.playerInSight) Attacking();
        else Patroling();
    }

    void Patroling() 
    {
        anim.SetBool("Attack", false);
        SniperRend.material = SniperPatroling;
        transform.LookAt(target);
    }
    void Attacking() 
    {
        anim.SetBool("Attack", true);
        SniperRend.material = SniperAttacking;
        transform.LookAt(player);
        if (!alreadyAttacked) 
        {
            Shoot();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void Shoot() 
    {
        FindObjectOfType<AudioManager>().Play("SniperShoot");
        GameObject pellet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRB = pellet.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        alreadyAttacked = true;
    }
    public void ResetAttack() => alreadyAttacked = false;
}
