using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController player;
    Vector3 groundPos;
    Vector3 lastGroundPos;
    string groundName;
    string lastGroundName;

    public Vector3 originOffSet;

    public float factorDiv = 1.1f;

    void Start()
    {
        player = this.GetComponent<CharacterController>();
    }

    
    void Update()
    {
        if (player.isGrounded)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position + originOffSet, player.radius / factorDiv, -transform.up, out hit))
            {
                GameObject GroundedIn = hit.collider.gameObject;
                groundName = GroundedIn.name;
                groundPos = GroundedIn.transform.position;

                if (groundPos != lastGroundPos && groundName == lastGroundName)
                {
                    this.transform.position += groundPos - lastGroundPos;
                    player.enabled = false;
                    player.transform.position = this.transform.position;
                    player.enabled = true;
                }

                lastGroundName = groundName;
                lastGroundPos = groundPos;
            }

        }
        else if (!player.isGrounded)
        {
            lastGroundName = null;
            lastGroundPos = Vector3.zero;
        }
        
    }

    private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + originOffSet, player.radius / factorDiv);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

}
