using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBound : MonoBehaviour
{
    public string objTag;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == objTag)
        {
            Debug.Log("### LeftBound Collision ### ---- " + other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
