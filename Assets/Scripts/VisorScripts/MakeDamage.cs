using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public int amount = 5;
    [SerializeField] ParticleSystem AttackPE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            ParticleAttack();
            FindObjectOfType<AudioManager>().Play("VisorPunch");
            other.GetComponent<HAD>().SubstractLife(amount);
        }
    }

    private void ParticleAttack() 
    {
        AttackPE.Play();
    }
}
