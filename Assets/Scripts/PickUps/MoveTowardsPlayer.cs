using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private GameObject Player;
    public float Maxpeed = 20.0f;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            speed += 1;
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
        }

    }
}
