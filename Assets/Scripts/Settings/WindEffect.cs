using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    //public Rigidbody rb;
    private GameObject Player;
    private Rigidbody2D m_PlayerRb;

    private bool m_hitWind = false;
    public bool flip = false;

    public float Force = 30.0f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerRb = Player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(m_hitWind)
        {
            if(flip)
            {
                m_PlayerRb.AddForce(transform.right * Force);
            }
            else
            {                
                m_PlayerRb.AddForce(-transform.right * Force);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
             m_hitWind = true;
             StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        m_hitWind = false;
    }
}
