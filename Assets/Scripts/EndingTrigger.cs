using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    Transform player;
    Collider col;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        col = this.GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            col.enabled = false;

            switch (this.gameObject.tag)
            {
                case "NormalEnding":
                    FindObjectOfType<GameManager>().NormalEnding();
                    break;
                case "PrismaticEnding":
                    FindObjectOfType<GameManager>().PrismaticEnding();
                    break;
                default:
                    break;
            }
        }
    }
}
