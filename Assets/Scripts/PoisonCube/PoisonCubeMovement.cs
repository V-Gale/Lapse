using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCubeMovement : MonoBehaviour
{
    public Rigidbody rb;

    public Transform[] PCubePos;
    public float platformSpeed = 3.5f;
    public int currentPos = 0;
    public int nextPos;
    public bool moveToNext = true;
    public float waitTime = 1.5f;
    void Update()
    {
        PoisonCubeMove();
    }

    void PoisonCubeMove() 
    {
        if (moveToNext == true)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, PCubePos[nextPos].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(rb.position, PCubePos[nextPos].position) <= 0)
        {

            currentPos = nextPos;
            nextPos++;
            if (nextPos > PCubePos.Length - 1)
            {
                nextPos = 0;
            }
        }
   
    }
}
