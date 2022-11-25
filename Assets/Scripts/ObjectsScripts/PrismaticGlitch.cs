using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaticGlitch : MonoBehaviour
{
    ParticleSystem glitchPS;
    public Transform prismaticPH;
    private void Awake()
    {
        glitchPS = this.GetComponent<ParticleSystem>();
        prismaticPH = GameObject.FindGameObjectWithTag("PrismaticPH").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Orb") 
        {
            prismaticPH.GetComponent<PrismaticCard>().GlitchActive();
            FindObjectOfType<AudioManager>().Play("GlitchDestroyed");
            Destroy(glitchPS);
            Destroy(gameObject);
        }
    }
}
