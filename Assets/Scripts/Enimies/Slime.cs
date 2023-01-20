using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    private Rigidbody2D SlimeRB;

    public AudioSource SlimeSound;
    // Start is called before the first frame update
    void Start()
    {
        SlimeRB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "LEFTSCREEN")
        {
            SlimeRB.AddForce(-transform.right * 2000);
            GetComponent<BoxCollider2D> ().enabled = false;
        }
        if(other.gameObject.tag == "Ground")
        {
            SlimeSound.Play();
            SlimeRB.AddForce(transform.up * 5000);
            SlimeRB.AddForce(-transform.right * 800);
        }
        else if(other.gameObject.tag == "Player") 
        {
            SlimeRB.AddForce(-transform.right * 1000);
        }

    }
}
