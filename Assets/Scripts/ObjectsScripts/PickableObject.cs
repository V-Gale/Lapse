using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isPickable = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject; 
            /*Le estamos diciendo en el if que busque si lo que colisionó con el objeto es la zona de interacción del player y que si es,
             que busque en el componente padre del player el script de PickUpObject y asignarle como objeto pickeable el objeto actual.
            (this.gameObject)*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = null; //Cuando sale de la interacción no marca el objeto.           
        }
    }
}
