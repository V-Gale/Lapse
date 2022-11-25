using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaticCard : MonoBehaviour
{
    public GameObject prismaticCard;
    int glitchCount = 0;
    ParticleSystem prismPS;

    private void Awake()
    {
        prismPS = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (glitchCount == 3) 
        {
            Destroy(prismPS);
            Instantiate(prismaticCard, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void GlitchActive() 
    {
        glitchCount += 1;
    }
}
