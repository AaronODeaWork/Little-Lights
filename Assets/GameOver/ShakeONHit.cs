using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeONHit : MonoBehaviour
{

    private GameObject Player;
    private GameObject Camera;
    private Shake CameraShake;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("Camera");
        CameraShake= Camera.GetComponent<Shake>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            CameraShake.TriggerShake(0.2f, 0.01f);
        }
    }
}
