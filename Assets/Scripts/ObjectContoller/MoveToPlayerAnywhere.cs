using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerAnywhere : MonoBehaviour
{
    private GameObject Player;
    public float Maxpeed = 50.0f;
    public float speedIncrease = 5.0f;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        speed += speedIncrease;
        if(speed >= Maxpeed)
        {
            speed = Maxpeed;
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
    }
}
