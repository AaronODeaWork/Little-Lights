using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMove : MonoBehaviour
{
    public float m_Speed = 1f;
    public float m_DeletePosition = -70.0f;

    public float m_SpeedMin = 1f;
    public float m_SpeedMax = 5.0f;

    public bool random = false;

    // Start is called before the first frame update
    void Start()
    {
        if(random)
        {
            m_Speed =  Random.Range(m_SpeedMin, m_SpeedMax);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -Time.deltaTime*m_Speed,0, 0, Space.World);
        
        if(transform.position.x  < m_DeletePosition)
        {
            Destroy(gameObject);  
        }
    }
}
