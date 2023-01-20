using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHand : MonoBehaviour
{
    private GameObject Player;
    public GameObject m_DarkHand;
    public GameObject m_DarkHandCurrent;
    
    public GameObject m_returnPoint;
    public float Maxpeed = 0.15f;
    public float speedIncrease = 0.05f;
    public float m_currentSpeed = 0.0f;

    public float m_HandBackpeed = 2.0f;

    private Vector3 StartPosition;

    bool m_MoveHand = false;
    bool m_ReturnHand = false;

    private  Vector3 vectorToTarget;
    private Quaternion RotationQuaternion; 
    private float HandAngle;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartPosition = m_returnPoint.transform.position;
    }

 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_ReturnHand)// move player back to its position
        {   
            StartPosition = m_returnPoint.transform.position;
            m_DarkHand.transform.position = Vector2.MoveTowards(m_DarkHand.transform.position, StartPosition, m_HandBackpeed);
            if( m_DarkHand.transform.position == StartPosition)
            {
                m_ReturnHand = false;
                m_currentSpeed = 0.0f;
            }
        }
        else if(m_MoveHand)// move hand if player is in trigger
        {
            vectorToTarget = m_DarkHand.transform.position- Player.transform.position;
            HandAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
 
            RotationQuaternion = Quaternion.AngleAxis(HandAngle, Vector3.forward);
            m_DarkHand.transform.rotation = Quaternion.Slerp(m_DarkHand.transform.rotation, RotationQuaternion, Time.deltaTime * m_currentSpeed*4);

            if(m_currentSpeed < Maxpeed)
            {
                m_currentSpeed += speedIncrease* Time.deltaTime;
            }
            m_DarkHand.transform.position = Vector2.MoveTowards(m_DarkHand.transform.position, Player.transform.position, m_currentSpeed);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.gameObject.tag == "Player")
        {
            if(!m_MoveHand)
            {
                StartCoroutine(WaitForStart());
            }
        }    
    }

    void  OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && m_DarkHandCurrent.activeSelf)
        {
            StartCoroutine(Wait());               
        }
    }

    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        m_MoveHand = false;     
        m_ReturnHand = true;

    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1);
        m_MoveHand = true;        
    }
    
}
