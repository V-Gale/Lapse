using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    Transform player;
    int amount = 100;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<HAD>().SubstractLife(amount);
        }        
    }
}
