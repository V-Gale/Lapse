using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rendering Materials:
    public Material RefCasting;
    public Material RefBaseColor;

    public Material BoostPlayer;
    public Material PlayerBaseColor;

    Renderer refRend;
    Renderer playerRend;

    //Animations:
    Animator anim;
   
    //Player movement: 
    public float horizontalMove;
    public float verticalMove;
    CharacterController player;
    public float pSpeed = 10f;
    Vector3 playerInput;
    public float jumpForce = 6f;

    //Movement based on camera position:
    public Camera cam;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer; //Also a variable of Player Movement.

    //Stamina
    public StaminaBar staminaBar;
    public int maxStamina = 100;
    public int currentStamina;
    [SerializeField]ParticleSystem playerPS;

    //Orb
    public Transform orbSpawnPoint;
    public GameObject orb;
    public float orbSpeed = 5f;
    public bool alreadyCast;
    public float timeBetweenCast = 0.2f;

    //Gravity
    public float gravity = 20f;
    public float fallVelocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        playerPS = GameObject.Find("PlayerStaminaParticles").GetComponent<ParticleSystem>();
        refRend = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
        playerRend = this.GetComponent<Renderer>();

    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        camDirection();
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * pSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
    }

    void camDirection() 
    {
        camForward = cam.transform.forward;
        camRight = cam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity() 
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else 
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    void PlayerSkills() 
    {
        //Jump
        if (player.isGrounded && Input.GetButton("Jump")) 
        {
            FindObjectOfType<AudioManager>().Play("PlayerJump");
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }

        //Run
        if (player.isGrounded && Input.GetKey(KeyCode.LeftShift) && player.velocity.magnitude != 0)
        {
            StaminaBar.instance.UsingStamina(1);
            StaminaBar.instance.CheckStamina();
            if (StaminaBar.instance.canRun)
            {
                playerRend.material = BoostPlayer;
                pSpeed = 10f;
                playerPS.Play();
                if (Input.GetKeyDown(KeyCode.LeftShift)) FindObjectOfType<AudioManager>().Play("PlayerStaminaPowerOn");
            }
            else if (!StaminaBar.instance.canRun)
            {
                playerRend.material = PlayerBaseColor;
                pSpeed = 6f;
                playerPS.Stop(); 
            }
        }
        else 
        {
            playerRend.material = PlayerBaseColor;
            pSpeed = 6f;
            playerPS.Stop();
            if(Input.GetKeyUp(KeyCode.LeftShift)) FindObjectOfType<AudioManager>().Play("PlayerStaminaPowerOff");
        }

        //Orb
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!alreadyCast) 
            {
                refRend.material = RefCasting;
                CastOrb();
                FindObjectOfType<AudioManager>().Play("PlayerOrb");
                Invoke(nameof(ResetOrbCasting), timeBetweenCast);
            }
        }
        else refRend.material = RefBaseColor;
      
    }

    void CastOrb() 
    {
        GameObject orbSpawn = Instantiate(orb, orbSpawnPoint.transform.position, orbSpawnPoint.transform.rotation);
        Rigidbody orbRb = orbSpawn.GetComponent<Rigidbody>();
        orbRb.AddForce(orbSpawnPoint.forward * orbSpeed, ForceMode.Impulse);
        alreadyCast = true;
    }
    public void ResetOrbCasting() => alreadyCast = false;

    private void OnAnimatorMove()
    {
        
    }

}
