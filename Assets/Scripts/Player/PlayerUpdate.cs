using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUpdate : MonoBehaviour
{
    private GameObject Player;//refrence to the player object. 
    private UnityEngine.Experimental.Rendering.Universal.Light2D PlayerLight;//refrence to the player light
    private TrailRenderer tr;// refrence to the players traile rendere
    private AnimationCurve curve = new AnimationCurve();

    public ParticleSystem PlayerParticals;
    public ParticleSystem DeathParticals;

    public GameObject[] Gems = new GameObject[5];//Gems that make a player gem


    public GameObject FadeOutDeath;
    private float DeathFadeOut = 0;
    public float DeathFadeOutSpeed = 0.01f;


    public float MaxHealth = 100;
    public float Health;
    public GameObject HealthPrefab;

    //Damages for each enemy 
    public float DarkHandDamage = 0.2f;
    public float BatDamage = 0.01f;
    public float slimeDamage = 0.1f;
    public float FireBallDamage = 5.0f;
    public float GroundHandDamage = 0.2f;

    
    public bool m_Alive = true;//Player alive bool
    private  bool m_AliveFinished  = false;// Bool for death animation

    private int Level = 0;// Gem level (gems top right)
    private int LevelUpAmount = 10;// amount of pickups need to level up default    
    
    public int[] LevelUpAmountPerLevel  = new int[6];//Amount of pickups need to level up per level

    private GameObject[] HP_Orbs = new GameObject[150];// countainer for Orbs in the hp flask
    private GameObject HP_CONTAINER;//HP container

    private GameObject[] PickUp_Orbs = new GameObject[500];//Pick up orbs array for score flask

    public bool m_GemPause = false;//Used to slow down collecting gems 

    private bool AudioDeathFade = true;//Audio death fade
    private GameObject Audio_Controller;
    private AudIoController Audio_Scripth;

    public AudioSource DeathSound;


    private GameObject GemObj;
    private AudioSource Gem_sound;


    private int WorldLevel = 0;// world gem level

    // Start is called before the first frame update
    void Start()
    {
        LevelUpAmount = LevelUpAmountPerLevel[0];

        GemObj = GameObject.FindGameObjectWithTag("GemUpgrade");
        Gem_sound = GemObj.GetComponent<AudioSource>();

        Audio_Controller = GameObject.FindGameObjectWithTag("AUDIO_CONTROLLER");
        Audio_Scripth = Audio_Controller.GetComponent<AudIoController>();

        Health = MaxHealth;//Set initial health 

        Player = GameObject.FindGameObjectWithTag("Player");//get player refrence 
        PlayerLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        tr = GetComponent<TrailRenderer>();

        DeathParticals.Stop();
        HP_CONTAINER = GameObject.FindGameObjectWithTag("HP_CONTAINER");
    }

    void FixedUpdate()
    {

        //Update the tail of the player to match current health 
        curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.5f);
        curve.AddKey(Health/100, 0.0f);
        tr.widthCurve = curve;

        PlayerLight.intensity =  Health/100;// set player light based on current health


        HP_Orbs = GameObject.FindGameObjectsWithTag("HP_ORBS");
        PickUp_Orbs = GameObject.FindGameObjectsWithTag("PickUp_ORBS");


        if(PickUp_Orbs.Length > LevelUpAmount && !m_GemPause)// check if player has enough to level
        {
            for (int i = 0; i < PickUp_Orbs.Length; i++)
            {
                Rigidbody2D new_FireFlyRB = PickUp_Orbs[i].GetComponent<Rigidbody2D>();
                new_FireFlyRB.AddForce(transform.up *200);
            }
            m_GemPause = true;
            Level ++;
            Gem_sound.Play();
            //Set next gem active 
            switch (Level)
            {  
                default:
                case 0:
                break;
                case 1:
                    Gems[0].SetActive(true);
                break;
                case 2:
                    Gems[1].SetActive(true);
                break;
                case 3:
                    Gems[2].SetActive(true);
                break;
                case 4:
                    Gems[3].SetActive(true);
                break;
                case 5:
                    Gems[4].SetActive(true);
                break;
            }
        }
        
        if(HP_Orbs.Length > Health)// remove orbs if player loses Health
        {
            Destroy(HP_Orbs[HP_Orbs.Length-1]);
        }
        else if(HP_Orbs.Length <= Health)// increase Health orbs if player increases health
        {
            GameObject new_HP = Instantiate(HealthPrefab, Vector3.zero, Quaternion.identity);
            new_HP.transform.position = HealthPrefab.transform.position;
            new_HP.transform.parent = HP_CONTAINER.transform;
        }

        if(!m_Alive)//is player dies fade out music and screen
        {
            if(AudioDeathFade)
            {
                Audio_Scripth.FadeOutGameMusic();
                Audio_Scripth.FadeOutBossMusic();
                AudioDeathFade = false;
            }
            

            DeathFadeOut += DeathFadeOutSpeed; 
            FadeOutDeath.GetComponent<SpriteRenderer>().color  = new Color(0.0f,0.0f,0.0f,DeathFadeOut);
            
            if(DeathFadeOut >= 1.2f)
            {
                SceneManager.LoadScene (sceneName:"GameOver");
            }
        }
       
    }

    //Deals with when a player hits an enemy
    void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "DarkHand" && m_Alive)
        {
            Health -= DarkHandDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }

        if(other.gameObject.tag == "Bat" && m_Alive)
        {
            Health -= BatDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }
        
        if(other.gameObject.tag == "Slime" && m_Alive)
        {
            Health -= slimeDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }

        if(other.gameObject.tag == "Crow" && m_Alive)
        {
            Health -= BatDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }

        if(other.gameObject.tag == "GroundHand" && m_Alive)
        {
            Health -= GroundHandDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }

        if(other.gameObject.tag == "FireBall" && m_Alive)
        {
            Health -= FireBallDamage;
            if (Health < 1f)
            {
                Health = 0.0f;
                KillPlayer();
                PlayerParticals.Stop();
            }
        }
        
    }

    public bool getAlive()
    {
        return m_Alive;
    }

    public void ResetLevel()
    {        
        Level = 0;
        m_GemPause = false;
    }
    
    void updateLevelAmount()// loop through the gems and change them to have the correct pick up amount 
    {   
        DrawInFireFlys[] GemScripth = new DrawInFireFlys[5];
        LevelUpAmount = LevelUpAmountPerLevel[WorldLevel];
        for (int i = 0; i < Gems.Length; i++)
        {
            GemScripth[i] = Gems[i].GetComponent<DrawInFireFlys>();
            GemScripth[i].setLevelUpAmount(LevelUpAmount);
        }
    }

    public void setWorldLevelAmount(int Level)
    {   
        
        WorldLevel = Level;
        updateLevelAmount();
    }


    void KillPlayer()
    {
        m_Alive = false;

        if(!m_Alive && !m_AliveFinished)// if Player dies start animation
        {
            m_AliveFinished = true;
            DeathSound.Play();
            Player.GetComponent<SpriteRenderer>().color  = new Color(0.2f,0.2f,0.2f,255);
            Player.GetComponent<TrailRenderer>().enabled = false;
            Player.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
            Player.GetComponent<PlayerMove>().enabled = false;
            
            DeathParticals.Play();
        }
    }
}

