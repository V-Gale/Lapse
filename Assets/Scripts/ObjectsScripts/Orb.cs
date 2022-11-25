using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public ParticleSystem orbPS;
    private void Awake()
    {
        orbPS = GameObject.Find("OrbParticles").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") 
        { 
            Destroy(gameObject);
        }
    }
}
