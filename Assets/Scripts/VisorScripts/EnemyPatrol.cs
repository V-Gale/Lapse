using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    int waypointsIndex;
    public int waypointsCant;
    public Vector3 target;
    private float distanceTreshHold = 0.1f;
    public NavMeshAgent enemyAgent;
    public void EnemyPatroling()
    {
        enemyAgent.speed = 2f;
        UpdateDestination();
        if (Vector3.Distance(transform.position, target) < waypointsCant && Vector3.Distance(transform.position,target) < distanceTreshHold)
        {
            IterateWayPointIndex();            
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointsIndex].position;  
        transform.position = Vector3.MoveTowards(transform.position , target , enemyAgent.speed * Time.deltaTime);
        transform.LookAt(target);
    }

    private void IterateWayPointIndex()
    {
        waypointsIndex++;
        if (waypointsIndex == waypoints.Length)
        {
            waypointsIndex = 0;
        }
    }
}
