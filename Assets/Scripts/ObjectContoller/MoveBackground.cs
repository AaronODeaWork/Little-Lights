using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public GameObject m_backGround;

    public float m_Speed = 10.0f;
    public float m_DeletePosition = -30.0f;

    private GameObject BACKGROUND_CONTAINER;

    // Start is called before the first frame update
    void Start()
    {
        BACKGROUND_CONTAINER = GameObject.FindGameObjectWithTag("BACKGROUND_CONTAINER");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -Time.deltaTime*m_Speed,0, 0, Space.World);
        
        if(transform.position.x  < m_DeletePosition)
        {
            GameObject new_backGround = Instantiate(m_backGround, Vector3.zero, Quaternion.identity);
            new_backGround.transform.parent = BACKGROUND_CONTAINER.transform;
            new_backGround.transform.position = new Vector3(135.85f,20.0f,0);
            Destroy(gameObject);  
        }
    }
}
