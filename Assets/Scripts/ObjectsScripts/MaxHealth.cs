using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : MonoBehaviour
{
    public int amountRecover = 100;
    [SerializeField] ParticleSystem maxHealthPS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<HAD>().currentHealth < 100)
        {
            other.GetComponent<HAD>().AddLife(amountRecover);

            Destroy(gameObject);
            Instantiate(maxHealthPS, transform.position, transform.rotation).Play();
            Destroy(maxHealthPS);
            
        }
    }
}
