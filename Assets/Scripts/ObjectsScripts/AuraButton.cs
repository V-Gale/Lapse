using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraButton : MonoBehaviour
{
    public Material buttonActive;
    Renderer buttonRend;
    GameObject auraBox;
    Collider col;

    public ParticleSystem buttonPS;
    private void Awake()
    {
        buttonRend = this.GetComponent<Renderer>();
        auraBox = this.gameObject.transform.GetChild(2).gameObject;
        buttonPS = this.gameObject.GetComponentInChildren<ParticleSystem>();
        col = this.GetComponent<Collider>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Orb") 
        {
            buttonRend.material = buttonActive;
            FindObjectOfType<AudioManager>().Play("AuraButtonActivated");
            Instantiate(buttonPS, this.transform.position, this.transform.rotation).Play();    
            Destroy(auraBox);
            Destroy(buttonPS);
            col.enabled = false;
        }
    }
}
