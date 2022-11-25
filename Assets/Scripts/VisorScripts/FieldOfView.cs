using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 7f;
    [Range(0,360)]
    public float angle = 70f;
    public GameObject player;
    public LayerMask playerMask;
    public LayerMask obstructionMask;
    public bool playerInSight;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOV());
    }

    private IEnumerator FOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f); 
        while (true) 
        {
            yield return wait;
            ViewField();
        }
    }

    private void ViewField() 
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position,radius,playerMask);
        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) playerInSight = false;
                else playerInSight = true;                   
            }
            else playerInSight = false;
        }
        else if (playerInSight) playerInSight = false;
    }

}
