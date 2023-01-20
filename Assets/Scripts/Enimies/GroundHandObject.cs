using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandObject : MonoBehaviour
{
    public float UpdateOnFrame_forkill = 100;
    private float updateCount = 0;
    public float MinSpeed = 2.0f;
    public float MaxSpeed = 4.0f;
    private float Speed = 2.0f;

    private bool MoveDown = false;
    private bool MoveFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(MinSpeed,MaxSpeed);
        transform.position = new Vector3( transform.position.x + Random.Range(-1,1),transform.position.y,0);
    }

    // Update is called once per frame
    void Update()
    {
        updateCount++;
        if(updateCount > UpdateOnFrame_forkill && !MoveDown)
        {
            if(transform.position.y < -18)
            {
                transform.Translate( 0,Time.deltaTime*Speed, 0, Space.World);    
            }
            else
            {
                MoveFinished = true;
            }
        }
        
        if(updateCount > UpdateOnFrame_forkill*2 && MoveFinished)
        {
            MoveDown = true;
        }
        
        if(MoveDown)
        {
            if(transform.position.y > -21.5)
            {
                transform.Translate( 0,-Time.deltaTime*Speed, 0, Space.World);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
