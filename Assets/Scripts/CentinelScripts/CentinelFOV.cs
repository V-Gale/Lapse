using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentinelFOV : MonoBehaviour
{
    public float angle = 360f;
    [Range(0, 360)]
    public float radius;
    public GameObject player;
    public LayerMask playerMask;
    public LayerMask obstructionMask;
    public bool playerInSight;

   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CentFOV());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CentFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            CentViewField();
        }
    }

    private void CentViewField()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, playerMask);
        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) playerInSight = false;
                else playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
        else if (playerInSight) playerInSight = false;
    }
}
