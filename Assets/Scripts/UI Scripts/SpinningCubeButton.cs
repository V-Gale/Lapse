using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCubeButton : MonoBehaviour
{
    public GameObject cube;
    public RectTransform[] cubePH;
    //GameObject spCube;
    public List<GameObject> mycubes = new List<GameObject>();
    private void Awake()
    {
        //cubePH = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
    }
    private void OnMouseEnter()
    {       
        //spCube = Instantiate(cube, cubePH.position,cubePH.rotation);
        foreach (RectTransform trans in cubePH) 
        {
            mycubes.Add(Instantiate(cube, trans.position, trans.rotation) as GameObject);
        }
    }

    private void OnMouseExit()
    {
        DestroyCube();
    }

    public void DestroyCube()
    {
        foreach (GameObject obj in mycubes) 
        {
            Destroy(obj);
        }
    }
}
