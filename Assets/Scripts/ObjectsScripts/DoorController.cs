using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;
    [SerializeField]ParticleSystem doorPS;
    Transform player;

    public Material DoorOpen;
    public Material DoorClosed;

    Renderer doorLock1;
    Renderer doorLock2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        doorLock1 = this.gameObject.transform.GetChild(1).GetComponent<Renderer>();
        doorLock2 = this.gameObject.transform.GetChild(2).GetComponent<Renderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {

        switch (this.gameObject.tag) 
        {
            case "RedDoor":
                if (other.tag == "RedCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "YellowDoor":
                if (other.tag == "YellowCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "GreenDoor":
                if (other.tag == "GreenCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "BlueDoor":
                if (other.tag == "BlueCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "VioletDoor":
                if (other.tag == "VioletCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "WhiteDoor":
                if (other.tag == "WhiteCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            case "PrismaticDoor":
                if (other.tag == "PrismaticCard")
                {
                    DoorOpening();
                }
                else break;
                break;
            default:
                player.GetComponent<PickUpObject>().PickingObject();
                break;

        }

        void DoorOpening()
        {
            anim.SetTrigger("DoorOpen");
            Destroy(other.gameObject);
            doorLock1.material = DoorOpen;
            doorLock2.material = DoorOpen;
            FindObjectOfType<AudioManager>().Play("DoorOpen");
            doorPS = this.GetComponentInChildren<ParticleSystem>();
            Instantiate(doorPS, transform.position, transform.rotation).Play();
            Destroy(doorPS);
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

   

}
