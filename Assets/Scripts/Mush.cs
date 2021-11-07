using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mush : MonoBehaviour
{   
    void Update()
    {
        
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            Debug.Log("### LeftBound Collision ### ---- this: " + this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
