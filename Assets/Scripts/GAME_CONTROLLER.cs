using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_CONTROLLER : MonoBehaviour
{
    public GameObject m_Moon;
    private float m_MoonAngle;

    public float m_OrbitSpeed = 1.0f;

    private int worldLevel = 0;
    public GameObject m_cloud;
    public GameObject m_grass;
    public GameObject m_lamp;
    public GameObject m_pickUp;
    public GameObject m_enemyPrefab;
    public GameObject m_CrowPrefab;
    public GameObject m_SlimePrefab;

    public GameObject m_TreeBoss;

    public GameObject m_DarkHandOne;
    public GameObject m_DarkHandTwo;

    private GameObject[] m_currentLamps = new GameObject[10];

    public float m_cloudSpawnChancePercent = 10;
    public float m_grassSpawnChancePercent = 4;
    public float m_pickUpSpawnChancePercent = 50;
    public float m_enemySpawnChancePercent = 10;
    public float m_SlimeSpawnChancePercent = 5;

    public float m_lampSpaceingTime = 50.0f;

    private float m_currentpickUpChancePercent;
    private float m_currentenemyChancePercent;
    private float m_currentSlimeChancePercent;

    private GameObject CLOUD_CONTAINER;
    private GameObject GROUND_CONTAINER;
    private GameObject PICKUP_CONTAINER;
    private GameObject ENEMY_CONTAINER;

    private float m_Running = 0;

    private enum TIME_OF_DAY {MoonRise, MidNight, MoonSet,DarkNight,SunRise,MidDay,SunSet,DarkNightTwo};
    private     TIME_OF_DAY currentTime;

    private bool SlimeWait = false;
    private int SlimeWaitCount = 0;

    private bool BossTime = false;

    private bool TurnOffObjects = false;
    private int TurnOffHands = 0;


    private DefaultMove[] scriptsDefaultMove = new DefaultMove[99];
    private GroundController[] scriptsGround = new GroundController[5];
    private MoveBackground[] scriptsBG = new MoveBackground[30];



    // Start is called before the first frame update
    void Start()
    {    
        m_currentpickUpChancePercent = m_pickUpSpawnChancePercent;
        m_currentenemyChancePercent = m_enemySpawnChancePercent;
        m_currentSlimeChancePercent = m_SlimeSpawnChancePercent;

        currentTime = TIME_OF_DAY.MoonRise;

        CLOUD_CONTAINER = GameObject.FindGameObjectWithTag("CLOUD_CONTAINER");
        GROUND_CONTAINER= GameObject.FindGameObjectWithTag("GROUND_CONTAINER");
        PICKUP_CONTAINER= GameObject.FindGameObjectWithTag("PICKUP_CONTAINER");
        ENEMY_CONTAINER= GameObject.FindGameObjectWithTag("ENEMY_CONTAINER");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!BossTime)
        {
            
            calculateTime();
            SpawnItems();
            SpawnFireFly();
        }
        else
        {
            if(TurnOffObjects)
            {      
                m_TreeBoss.SetActive(true);

                TurnOffObjects = false;
                TurnOffHands = 1;

                scriptsDefaultMove = Object.FindObjectsOfType(typeof(DefaultMove)) as DefaultMove[];
                for (int i = 0; i < scriptsDefaultMove.Length; i++)
                {
                    scriptsDefaultMove[i].enabled = false;
                }

                scriptsGround = Object.FindObjectsOfType(typeof(GroundController)) as GroundController[];
                for (int i = 0; i < scriptsGround.Length; i++)
                {
                    scriptsGround[i].enabled = false;
                }

                scriptsBG = Object.FindObjectsOfType(typeof(MoveBackground)) as MoveBackground[];
                for (int i = 0; i < scriptsBG.Length; i++)
                {
                    scriptsBG[i].enabled = false;
                }
                StartCoroutine(turmOffHands());
            }

            if(TurnOffHands == 1)
            {
                m_DarkHandOne.transform.position = Vector2.MoveTowards(m_DarkHandOne.transform.position, new Vector3(0,300,0), 4);
                m_DarkHandTwo.transform.position = Vector2.MoveTowards(m_DarkHandTwo.transform.position, new Vector3(0,300,0), 4);
            }
        }

    }

    void calculateTime()
    {
        m_MoonAngle = m_Moon.transform.rotation.eulerAngles.z;
        switch (currentTime)
        {            
            default:

            //==========================================================MoonRise
            case TIME_OF_DAY.MoonRise:

                SpawnCrow();
                if(m_MoonAngle < 345 && m_MoonAngle >= 310)//Mid night 
                {
                    currentTime = TIME_OF_DAY.MidNight;
                }
            break;

            //==========================================================MidNight
            case TIME_OF_DAY.MidNight:
                if(worldLevel >= 1)
                {
                SpawnBat();
                }
                if(m_MoonAngle < 310 && m_MoonAngle >= 275)//Moon set
                {
                    currentTime = TIME_OF_DAY.MoonSet; 
                }
            break;

            //==========================================================MoonSet
            case TIME_OF_DAY.MoonSet:
                if(worldLevel >= 1)
                {
                    SpawnBat();
                }
                if(m_MoonAngle < 275 && m_MoonAngle >= 225)//Dark night
                {                    
                    currentTime = TIME_OF_DAY.DarkNight; 
                    m_currentenemyChancePercent = m_enemySpawnChancePercent*1.5f;
                    m_currentpickUpChancePercent = m_pickUpSpawnChancePercent*3;
                }
            break;

            //==========================================================DarkNight
            case TIME_OF_DAY.DarkNight:
                if(worldLevel >= 1)
                {
                    SpawnBat();
                }
                if(m_MoonAngle < 225 && m_MoonAngle >= 180)//Sun rise
                {
                    currentTime = TIME_OF_DAY.SunRise; 
                    m_currentenemyChancePercent = m_enemySpawnChancePercent/2;
                    m_currentpickUpChancePercent = m_pickUpSpawnChancePercent/2;

                }

            break;

            //==========================================================SunRise
            case TIME_OF_DAY.SunRise:
 
                SpawnCrow();
                if(worldLevel >= 2)
                {
                    SpawnSlime();
                }
                m_currentLamps = GameObject.FindGameObjectsWithTag("Lamp");
                for (int i = 0; i < m_currentLamps.Length; i++)
                {  
                    m_currentLamps[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false; 
                    m_currentLamps[i].GetComponent<Collider2D>().enabled = false;
                }

                if(m_MoonAngle < 180 && m_MoonAngle >= 140 )//Mid Day
                {
                    currentTime = TIME_OF_DAY.MidDay; 
                    m_currentenemyChancePercent = 1;
                    m_currentpickUpChancePercent = 5;

                }
            break;

            //==========================================================MidDay
            case TIME_OF_DAY.MidDay:
            
                SpawnCrow();
                if(worldLevel >= 2)
                {
                    SpawnSlime();
                }
                if(m_MoonAngle < 140 && m_MoonAngle >= 105)//Sun Set
                {
                    currentTime = TIME_OF_DAY.SunSet;
                    m_currentenemyChancePercent = m_enemySpawnChancePercent;
                    m_currentpickUpChancePercent = m_pickUpSpawnChancePercent;
                }
            break;

            //==========================================================SunSet
            case TIME_OF_DAY.SunSet:

                SpawnCrow();
                if(worldLevel >= 2)
                {
                    SpawnSlime();
                }
                m_currentLamps = GameObject.FindGameObjectsWithTag("Lamp");
                for (int i = 0; i < m_currentLamps.Length; i++)
                {  
                    m_currentLamps[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true; 
                    m_currentLamps[i].GetComponent<Collider2D>().enabled = true;
                }
                if(m_MoonAngle < 105 && m_MoonAngle >= 50)//Dark night 2
                {
                    currentTime = TIME_OF_DAY.DarkNightTwo;
                    m_currentenemyChancePercent = m_enemySpawnChancePercent*1.5f;
                    m_currentpickUpChancePercent = m_pickUpSpawnChancePercent*3;
                }

            break;

            //==========================================================Dark night 2
            case TIME_OF_DAY.DarkNightTwo:
                if(worldLevel >= 1)
                {
                    SpawnBat();
                }
                if(m_MoonAngle < 50 && m_MoonAngle >= 0 || m_MoonAngle < 360 && m_MoonAngle >= 345 )//Moon rise
                {
                    currentTime = TIME_OF_DAY.MoonRise;
                    m_currentenemyChancePercent = m_enemySpawnChancePercent;
                    m_currentpickUpChancePercent = m_pickUpSpawnChancePercent;
                }
            break;

        }
    }

    //Spawn world items like grass,lamps etc
    void SpawnItems()
    {

        if( Random.Range(1, 1000) <= m_cloudSpawnChancePercent)//cloud
        {
            GameObject new_cloud = Instantiate(m_cloud, Vector3.zero, Quaternion.identity);
            new_cloud.transform.parent = CLOUD_CONTAINER.transform;
        }

        if( Random.Range(1, 1000) <= m_grassSpawnChancePercent)//grass
        {    
            

            GameObject new_grass = Instantiate(m_grass, Vector3.zero, Quaternion.identity);
            new_grass.transform.position = m_grass.transform.position;
            new_grass.transform.parent = GROUND_CONTAINER.transform;
        }

        m_Running += Time.deltaTime;
        
        if(m_Running >m_lampSpaceingTime)//lamps
        {
            m_Running = 0;
            GameObject new_lamp = Instantiate(m_lamp, Vector3.zero, Quaternion.identity);
            new_lamp.transform.position = m_lamp.transform.position;
            new_lamp.transform.parent = GROUND_CONTAINER.transform;
        }
    }

    void SpawnBat()
    {
        if( Random.Range(1, 1000) <= m_currentenemyChancePercent)//enemy
        {    
            GameObject new_enemy = Instantiate(m_enemyPrefab, Vector3.zero, Quaternion.identity);
            new_enemy.transform.position = m_enemyPrefab.transform.position;
            new_enemy.transform.parent = ENEMY_CONTAINER.transform;
        }
    }
    void SpawnCrow()
    {
        if( Random.Range(1, 1000) <= m_currentenemyChancePercent)//enemy
        {    
            GameObject new_crow = Instantiate(m_CrowPrefab, Vector3.zero, Quaternion.identity);
            new_crow.transform.position = m_CrowPrefab.transform.position;
            new_crow.transform.parent = ENEMY_CONTAINER.transform;
        }
    }
    void SpawnSlime()
    {

        if( Random.Range(1, 1000) <= m_currentSlimeChancePercent && !SlimeWait)
        {   
            SlimeWait = true;
            GameObject new_Slime = Instantiate(m_SlimePrefab, Vector3.zero, Quaternion.identity);
            new_Slime.transform.position = m_SlimePrefab.transform.position;
            new_Slime.transform.parent = ENEMY_CONTAINER.transform;
        }
        if (SlimeWait)
        {
            SlimeWaitCount++;
            if(SlimeWaitCount >= 200)
            {
                SlimeWaitCount = 0;
                SlimeWait = false;
            }

        }
    }
    void SpawnFireFly()
    {
        if( Random.Range(1,1000) <= m_currentpickUpChancePercent)//fireflys
        {    
            GameObject new_pickUp = Instantiate(m_pickUp, Vector3.zero, Quaternion.identity);
            new_pickUp.transform.parent = PICKUP_CONTAINER.transform;
            new_pickUp.transform.position = new Vector3(40,Random.Range(-10, 20),0);
        }
    }

    public void setBossActive()
    {
        TurnOffObjects = true;
        BossTime = true;
        TurnOffHands = 2;
    }

    private IEnumerator turmOffHands()
    {
        yield return new WaitForSeconds(2);
        m_DarkHandOne.SetActive(false);
        m_DarkHandTwo.SetActive(false);
    }

    public void UpdateWorldLevel(int t_WorldLevel)
    {
        worldLevel = t_WorldLevel;
    }
}
