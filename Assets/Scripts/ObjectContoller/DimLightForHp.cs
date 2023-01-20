using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimLightForHp : MonoBehaviour
{
    private GameObject Player;
    public GameObject LightPartical;

    private PlayerUpdate playerScript;
    private UnityEngine.Experimental.Rendering.Universal.Light2D LampLight;
    public float LampHealth = 50;
    private float CurrentLampHealth;

    public float playerHealthIncreasePS = 0.5f;

    private  bool LampAlive = true;

    public ParticleSystem Particals;

    private GameObject Absorbing;
    private AudioSource AbsorbingSound;

    private GameObject AbsorbingDone;
    private AudioSource AbsorbingDoneSound;

    private bool AbsorbStarted = true;
    // Start is called before the first frame update
    void Start()
    {
        Absorbing =   GameObject.FindGameObjectWithTag("AbsorbSound");
        AbsorbingDone =   GameObject.FindGameObjectWithTag("AbsorbDone");

        AbsorbingSound = Absorbing.GetComponent<AudioSource>();
        AbsorbingDoneSound = AbsorbingDone.GetComponent<AudioSource>();



        CurrentLampHealth = LampHealth+1;
        LampLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<PlayerUpdate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && LampAlive && playerScript.Health < 100.0f && playerScript.getAlive())
        {
            if(CurrentLampHealth>1.0f)
            {
                if(AbsorbStarted)
                {
                    AbsorbingSound.Play();
                    AbsorbStarted = false;
                }
                CurrentLampHealth -= playerHealthIncreasePS;
                playerScript.Health += playerHealthIncreasePS;    

                GameObject new_LightPartical = Instantiate(LightPartical, Vector3.zero, Quaternion.identity);
                new_LightPartical.transform.position = new Vector3(transform.position.x + Random.Range(-1,1),transform.position.y+ Random.Range(-1,1),0);
            }
            else
            {
                if(!AbsorbStarted)
                {
                    AbsorbStarted = true;
                    AbsorbingSound.Stop();
                    AbsorbingDoneSound.Play();
                }
                LampAlive = false;
                Particals.Stop();

            }
            LampLight.intensity =  CurrentLampHealth/LampHealth;
        }
        

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && AbsorbingSound.isPlaying)
        {
            
            if(!AbsorbStarted)
            {
                AbsorbStarted = true;
                AbsorbingSound.Stop();
            }
        }
    }
}
