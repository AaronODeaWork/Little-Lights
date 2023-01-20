using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        { 
            Destroy(gameObject);
        }
    }
}
