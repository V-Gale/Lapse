using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCube : MonoBehaviour
{
    GameObject poisonCube;
    Transform player;
    int amount = 5;
    ParticleSystem poisonPS;
    private void Awake()
    {
        poisonCube = this.gameObject.transform.GetChild(0).gameObject; 
        player = GameObject.Find("Player").GetComponent<Transform>();
        poisonPS = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>(); 
    }

    public void GetPoisoned() 
    {
        this.GetComponent<PoisonCubeMovement>().enabled = false;
        FindObjectOfType<AudioManager>().Play("PoisonCubeSound");
        Destroy(poisonCube);
        StartCoroutine(Poison());
    }

    IEnumerator Poison() 
    {
        for (int i = 0; i < 5; i++)
        {
            player.GetComponent<HAD>().SubstractLife(amount);
            Instantiate(poisonPS,player.transform.position, player.transform.rotation).Play();
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(poisonPS);
        Destroy(gameObject);
    }
}
