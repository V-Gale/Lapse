using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBridgePanels : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fog")
        {
            Debug.Log("DEATH PARADE!!!!!!!-------->");
            Destroy(gameObject);            
        }
    }
}
