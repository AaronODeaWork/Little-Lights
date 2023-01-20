using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnd : MonoBehaviour
{
    private Rigidbody2D Rb;

    public float framesTillStop = 100;
    private float updateCount = 0;

    private bool AllowJump = false;
    private bool StartThing = true;

    public GameObject FadeOutDeath;
    private float DeathFadeOut = 0;
    public float DeathFadeOutSpeed = 0.01f;


    private GameObject Audio_Controller;
    private AudIoController Audio_Scripth;

    // Start is called before the first frame update
    void Start()
    {

        Audio_Controller = GameObject.FindGameObjectWithTag("AUDIO_CONTROLLER");
        Audio_Scripth = Audio_Controller.GetComponent<AudIoController>();
        Audio_Scripth.FadeOutBossMusic();
        
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateCount++;
        if(updateCount>framesTillStop && StartThing)
        {
            AllowJump = true;
            StartThing = false;
            Rb.AddForce(transform.up * 3000);
            Rb.AddForce(transform.right * 1000);
        }

        if(transform.position.x >=40)
        {
            DeathFadeOut += DeathFadeOutSpeed; 
            FadeOutDeath.GetComponent<SpriteRenderer>().color  = new Color(0.0f,0.0f,0.0f,DeathFadeOut);
            
            if(DeathFadeOut >= 1.2f)
            {
                SceneManager.LoadScene (sceneName:"Win");
            }

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            if(AllowJump)
            {
                Rb.AddForce(transform.up * 3000);
                Rb.AddForce(transform.right * 1000);
            }
        }
    }
}
