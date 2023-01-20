using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private GameObject Player;
    private PlayerMove playerScript;

    public int DeleteOnPosition = -71;

    public int MinY = 0;
    public int MaxY = 20;

    public float Speed = 6.0f;

    public float UpdateOnFrame_PlayerPos = 100;
    private float UpdateOnFrame_PlayerPos_now;

    private float updateCount = 0;
    private Vector3 m_PlayerPosition;

    private  Vector3 vectorToTarget;
    private Quaternion RotationQuaternion; 
    private float BatAngle;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        UpdateOnFrame_PlayerPos_now =  Random.Range(0, UpdateOnFrame_PlayerPos);
        transform.position = new Vector2(45,Random.Range(MinY, MaxY));
    }

    void Update()
    {
        updateCount++;

        if(updateCount > UpdateOnFrame_PlayerPos)
        {
            updateCount = 0;
            m_PlayerPosition.y = Player.transform.position.y;
            if(m_PlayerPosition.y < 0)
            {
                m_PlayerPosition.y = -16;
            }
            m_PlayerPosition.x = Player.transform.position.x;
        }
    }
    void FixedUpdate()
    {
        if(transform.position.x > Player.transform.position.x+5)
        {
            vectorToTarget = transform.position- Player.transform.position;
            BatAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            if(BatAngle < -30)
            {BatAngle = -30;}
            else if(BatAngle > 30)
            {BatAngle = 30;}

            RotationQuaternion = Quaternion.AngleAxis(BatAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotationQuaternion, Time.deltaTime * Speed/4);
            transform.position = Vector2.MoveTowards(transform.position,m_PlayerPosition , Time.deltaTime*Speed);

        }
        else 
        {    
            RotationQuaternion = Quaternion.AngleAxis(0, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotationQuaternion, Time.deltaTime * Speed/4);
            transform.Translate( -Time.deltaTime*Speed,0, 0, Space.World);                
        }        

        if(transform.position.x < DeleteOnPosition)
        {
            Destroy(gameObject);  
        }

    }
}
