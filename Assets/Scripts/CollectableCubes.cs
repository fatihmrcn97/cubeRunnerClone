using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableCubes : MonoBehaviour
{

    public bool isCollected;
    private int index;

    public bool is2xObject=false;
    public GameObject text;

    public Collecter collecter;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
      
        if (isCollected)
        {
            if (transform.parent != null)
            {
                transform.localPosition = new Vector3(0, -index, 0);
       
            }
            if (is2xObject)
                text.SetActive(false);
        }
    }

    public void CollectedTrue()
    {
        isCollected = true;
      
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocking"))
        {
            cam.fieldOfView -= 2;
            collecter.HeightMinus();
            transform.parent = null;
            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
           
        }
        if (other.gameObject.CompareTag("endblock"))
        {
            transform.parent = null;
            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.tag = "try";
        }

    }

   
}
