using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float m_groundSpeed = 1f;
    public float m_goundDeletePosition = -70.0f;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -Time.deltaTime*m_groundSpeed,0, 0, Space.World);
        
        if(transform.position.x  < m_goundDeletePosition)
        {
            Destroy(gameObject);  
        }
    }
}
