using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody platformRb;
    public Transform[] PlatformPosition;
    public float platformSpeed = 3.5f;
    public int currentPos = 0;
    public int nextPos;
    public bool moveToNext = true;
    public float waitTime = 1.5f;
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveToNext == true) 
        {
            StopCoroutine(waitForMove(0));
            platformRb.MovePosition(Vector3.MoveTowards(platformRb.position, PlatformPosition[nextPos].position, platformSpeed * Time.deltaTime));
        }
        
        if (Vector3.Distance(platformRb.position, PlatformPosition[nextPos].position) <= 0) 
        {
            
            currentPos = nextPos;
            nextPos++;
            if (nextPos > PlatformPosition.Length - 1)
            {
                nextPos = 0;
            }
            else if (currentPos == 0 && nextPos == 1) 
            {
                StartCoroutine(waitForMove(waitTime));
            }
        }
        IEnumerator waitForMove(float time) //Siempre que se haga una rutina tiene que devolver algo porque sino da error.
        {
            moveToNext = false;
            yield return new WaitForSeconds(time);
            moveToNext = true;
        }
    }
}

