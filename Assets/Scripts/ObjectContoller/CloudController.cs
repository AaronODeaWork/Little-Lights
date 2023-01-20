using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{

    public float m_cloudSpeedMin = 1f;
    public float m_cloudSpeedMax = 5.0f;

    public float m_cloudDeletePosition = -70.0f;

    private SpriteRenderer sprite;
    private float m_cloudSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        m_cloudSpeed =  Random.Range(m_cloudSpeedMin, m_cloudSpeedMax);
        transform.position = new Vector3(60, Random.Range(5, 15), 0);

        sprite.sortingOrder = Random.Range(-1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -Time.deltaTime*m_cloudSpeed,0, 0, Space.World);

        if(transform.position.x  < m_cloudDeletePosition)
        {
            Destroy(gameObject);  
        }
    }
}
