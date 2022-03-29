using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePlatform : MonoBehaviour
{
  

    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, +1.5f, 0);
    }


}
