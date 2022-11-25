using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int amountRecover = 50;
    [SerializeField] ParticleSystem healthPS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<HAD>().currentHealth < 100) 
        {           
            other.GetComponent<HAD>().AddLife(amountRecover);
            Destroy(gameObject);
            Instantiate(healthPS, transform.position, transform.rotation).Play();
            Destroy(healthPS);
        }
    }
}
