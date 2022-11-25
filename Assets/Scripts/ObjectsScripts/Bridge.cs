using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject destroyedBridge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(destroyedBridge, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
