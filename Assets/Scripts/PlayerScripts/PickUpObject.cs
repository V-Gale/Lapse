using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;

    public Transform playerItemHolder;
    public Transform itemHolder;

    public Rigidbody rb;
    public Collider col;

    void Update()
    {
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickable == true && PickedObject == null && gameObject.GetComponent<CharacterController>().isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickingObject();
            }
        }
        else if (PickedObject != null) 
        {
            if (Input.GetKeyDown(KeyCode.Q) && gameObject.GetComponent<CharacterController>().isGrounded)
            {
                DroppingObject();
            }
        }
    }
    
    public void PickingObject() 
    {
        col = ObjectToPickUp.GetComponent<Collider>();
        col.isTrigger = true;
        PickedObject = ObjectToPickUp; //El objeto a agarrar es igual al que podemos agarrar 
        PickedObject.GetComponent<PickableObject>().isPickable = false; //Una vez que ya lo agarramos ya no lo podemos agarrar
        PickedObject.transform.SetParent(itemHolder);// Le decimos que se haga hijo de la zona de interacción del player
        PickedObject.transform.position = itemHolder.position;//Su posición es igual a la de la zona de interacción.
        PickedObject.GetComponentInChildren<ParticleSystem>().Stop();
        PickedObject.GetComponent<Rigidbody>().useGravity = false;//Una vez que lo tenemos no le afecta la gravedad.
        PickedObject.GetComponent<Rigidbody>().isKinematic = true;//Una vez que lo tenemos no le afectan las físicas.
        FindObjectOfType<AudioManager>().Play("CardPickUp");
        SetLayer(PickedObject, 5);
    }

    public void DroppingObject() 
    {
        col.isTrigger = false;
        PickedObject.GetComponent<PickableObject>().isPickable = true; //Al soltarlo es agarrable otra vez.
        FindObjectOfType<AudioManager>().Play("CardDrop");
        SetLayer(PickedObject, 0);
        PickedObject.transform.SetParent(playerItemHolder);
        PickedObject.transform.position = playerItemHolder.position;
        rb = PickedObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(playerItemHolder.transform.forward * 4f, ForceMode.Impulse);
        PickedObject.GetComponentInChildren<ParticleSystem>().Play();
        PickedObject.transform.SetParent(null);// Le decimos que se haga hijo de la nada cuando lo soltamos.                               
        PickedObject = null;
        rb.isKinematic = true;
        rb.useGravity = false;
        col.isTrigger = true;
    }

    public void SetLayer(GameObject obj, int layer) 
    {
        foreach (Transform child in obj.GetComponentsInChildren<Transform>(true)) 
        { 
            child.gameObject.layer = layer; 
        }
    }
}
