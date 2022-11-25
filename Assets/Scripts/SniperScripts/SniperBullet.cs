using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public Transform player;
    public int amount = 2;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            player.GetComponent<HAD>().SubstractLife(amount);
            Destroy(gameObject);
        }
        else if(other.tag != "Bullet") Destroy(gameObject);
    }
}
