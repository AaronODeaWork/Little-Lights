using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{    

    private GameObject Player;
    private GameObject new_FireBall;
    public GameObject FireBallPrefab;

    public GameObject FireBallLazerPrefab;

    private GameObject[] Boss_hp_orb = new GameObject[5];
    public GameObject[] Boss_hp_orbAll = new GameObject[5];


    public GameObject EyeOneOBJ;
    public GameObject EyeTwoOBJ;

    public UnityEngine.Experimental.Rendering.Universal.Light2D EyeOne;
    public UnityEngine.Experimental.Rendering.Universal.Light2D EyeTwo;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Mouth;

    public GameObject Nextlevel;

    public GameObject[] spawnPosition = new GameObject[5];
    public ParticleSystem [] EndParticals = new ParticleSystem [5];
    public bool [] startedPartical = new bool [5];


    public int BossLevel = 1;
    private Vector3 scaleChange;
    private float yScaleChange;
    private float xScaleChange;

    private float yScaleChangeStop;
    private float xScaleChangeStop;

    public float AnimationSpeed = 100;

    private float moveDownY;

    private int randomNumber;

    public float framesTillStop = 2000;
    private float updateCount = 0;

    public int FireBallSpawnRatePercent = 20;

    private bool Finished = false;

    public int FireBallCount = 0;
    public int FireBallCountMax = 500;


    public GameObject Camera;
    private Shake CameraShake;
    // Start is called before the first frame update
    void Start()
    {            
        for (int i = 0; i < EndParticals.Length; i++)
        {
            startedPartical[i] = false;
            EndParticals[i].Stop();
        }  
            
            xScaleChange = (transform.localScale.x - Nextlevel.transform.localScale.x)/AnimationSpeed;
            yScaleChange = (transform.localScale.y - Nextlevel.transform.localScale.y)/AnimationSpeed;
            scaleChange = new Vector3(xScaleChange ,yScaleChange, 0);
            yScaleChangeStop = Nextlevel.transform.localScale.x;
            xScaleChangeStop = Nextlevel.transform.localScale.y;
            moveDownY = (transform.position.y - Nextlevel.transform.position.y)/AnimationSpeed;

        Player = GameObject.FindGameObjectWithTag("Player");
        CameraShake= Camera.GetComponent<Shake>();

    }
    void FixedUpdate()
    {
        FireBallCount++;
        updateCount++;
        UpdateTreeState();

    }

    // Update is called once per frame
    void Update()
    {   
        if(FireBallCount > (FireBallCountMax-(FireBallCountMax/10)))
        {
            if(EyeOne.enabled == true)
            {
                EyeOne.enabled = false;
                EyeTwo.enabled = false;
            }
            else
            {
                EyeOne.enabled = true;
                EyeTwo.enabled = true;
            }

        }
        if(FireBallCount > FireBallCountMax)
        {

            if(Random.Range(0,2) == 0)
            {
                CameraShake.TriggerShake(0.1f, 0.2f);
                FireBallCount = 0;
                new_FireBall = Instantiate(FireBallLazerPrefab, Vector3.zero, Quaternion.identity);
                new_FireBall.transform.position = EyeOneOBJ.transform.position;
            }
            else
            {
                CameraShake.TriggerShake(0.1f, 0.2f);
                FireBallCount = 0;
                new_FireBall = Instantiate(FireBallLazerPrefab, Vector3.zero, Quaternion.identity);
                new_FireBall.transform.position = EyeTwoOBJ.transform.position;
            }
                EyeOne.enabled = true;
                EyeTwo.enabled = true;
        }

        if(!Finished && updateCount > framesTillStop )
        {
            updateCount = 0;
            for (int i = 0; i < spawnPosition.Length; i++)
            {
                if(Random.Range(0,100) < FireBallSpawnRatePercent && !startedPartical[i])
                {
                    new_FireBall = Instantiate(FireBallPrefab, Vector3.zero, Quaternion.identity);
                    new_FireBall.transform.position = spawnPosition[i].transform.position;
                }
            }
        }

    }

    void UpdateTreeState()
    {
        Boss_hp_orb = GameObject.FindGameObjectsWithTag("TreeBall");    

        EyeOne.intensity =  Boss_hp_orb.Length;
        EyeTwo.intensity =  Boss_hp_orb.Length;
        Mouth.intensity =  Boss_hp_orb.Length;
        
        for (int i = 0; i < Boss_hp_orbAll.Length; i++)
        {
            if(!Boss_hp_orbAll[i].activeSelf && !startedPartical[i] && !Nextlevel.activeSelf)
            {
                startedPartical[i] = true;
                EndParticals[i].Play();                
            }   
        }


        if( Boss_hp_orb.Length == 0 )
        {            
            if(transform.localScale.x <=  xScaleChangeStop && transform.localScale.y <= yScaleChangeStop)
            {
                Nextlevel.SetActive(true);
                Finished = true;

                CameraShake.TriggerShake(0.1f, 0.2f);
                for (int i = 0; i < EndParticals.Length; i++)
                {
                    if(startedPartical[i])
                    {
                        startedPartical[i] = false;
                        EndParticals[i].Stop();
                    }
                    StartCoroutine(WaitForDestroy());
                }  
            }
            else
            {          
                CameraShake.TriggerShake(0.01f, 0.01f);
                transform.localScale -= scaleChange;
                transform.position = new Vector3(transform.position.x,transform.position.y -moveDownY,0);
            }
        }
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
