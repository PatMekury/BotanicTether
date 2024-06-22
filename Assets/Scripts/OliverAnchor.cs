using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliverAnchor : MonoBehaviour
{
   public GameObject yourObject; // Reference to your 3D object
   public GameObject anchor; // Reference to your anchor

    // Start is called before the first frame update
    void Start()
    {
        yourObject.transform.SetParent(anchor.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
