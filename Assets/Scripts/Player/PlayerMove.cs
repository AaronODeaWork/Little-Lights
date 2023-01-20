using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    public float m_maxForwardThrust = 10f;
    public float m_maxUpThrust = 30.0f;
    public float m_forwardThrust = 1.0f;
    public float m_upThrust = 1.0f;


    private float m_currentUpThrust = 0.0f;
    private float m_currentForwardTrust = 1.0f;
    private float m_backThrust = 20.0f;

    protected Vector3 m_currentPosition;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    // Fixed Update is called a fix number of frames per second.
    void FixedUpdate()
    {
        if(Input.GetKey("space"))
        {
            if(m_currentForwardTrust <= m_maxForwardThrust)
            {
                m_currentForwardTrust += 1f;
                m_backThrust = m_currentForwardTrust;
            }
            if(m_currentUpThrust <= m_maxUpThrust)
            {
                m_currentUpThrust += m_upThrust;
            }
            
            m_Rigidbody.AddForce(transform.up * m_currentUpThrust);
            m_Rigidbody.AddForce(transform.right * m_currentForwardTrust);
        }
        else if(transform.position.x >-30)
        {
            if(m_currentForwardTrust <= 0)
            {
                m_currentForwardTrust -= 1f;
            }
            m_currentUpThrust = 0;
            m_Rigidbody.AddForce(-transform.right * m_backThrust);
        }


 
    }
    
    public Vector3 GetPosition()
    {
        return m_currentPosition;
    }
}
