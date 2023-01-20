using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGemParticals : MonoBehaviour
{
    public GameObject[] Gem_particals = new GameObject[5];
    public GameObject[] Gems = new GameObject[5];
    public GameObject[] Gems_player = new GameObject[5];
    public GameObject[] Gems_World_indicator = new GameObject[6];

    private MeshRenderer  Gem_player_Renderer;
    private UnityEngine.Experimental.Rendering.Universal.Light2D Gem_playerLight;

    public int playerLevel = 0;
    public int WorldLevel = 0;
    private bool TriggerWorldUpgrade = false;
    private bool TriggerWorldGemCombine = false;
    private bool TriggerWorldGemEnd = false;

    public float Force= 3.0f;

    private GameObject GEM_CONTAINER;
    private GameObject[] Gems_count = new GameObject[5];
    
    public float framesTillStop = 5;
    private float updateCount = 0;
    private float updateCountUpgarde = 0;
    public float updateCountUpgardeLength = 1000;
    private float updateCountEnd = 0;
    public float updateCountEndLength = 100;

    public float gemSpeedIncrease = 1.5f;
    public float gemSpeedStop = 1000.0f;

    private bool StartEndGems = false;
    private bool END_GAME_ACTIVE = false;

    private GameObject Player;
    private  GAME_CONTROLLER GameController;

    private PlayerUpdate PlayerScripth;
    private SpriteRenderer GemSpriteRenderer;
    private DrawInFireFlys GemDrawInFireFlys;

    private RotateAroundGivenPoint GemRotateScripth;

    private GameObject new_Gem_particals;
    private Rigidbody2D new_rb;

    private GameObject Audio_Controller;
    private AudIoController Audio_Scripth;

    private GameObject GemObj;
    private AudioSource Gem_sound;

    private GameObject WorldGemObj;
    private AudioSource WorldGem_sound;
    // Start is called before the first frame update
    void Start()
    {

        GemObj = GameObject.FindGameObjectWithTag("PlayerGemUpgrade");
        Gem_sound = GemObj.GetComponent<AudioSource>();

        WorldGemObj = GameObject.FindGameObjectWithTag("WorldGemUpgrade");
        WorldGem_sound = WorldGemObj.GetComponent<AudioSource>();



        GEM_CONTAINER = GameObject.FindGameObjectWithTag("GEM_CONTAINER");
        GameController = GameObject.FindGameObjectWithTag("GAME_CONTROLLER").GetComponent<GAME_CONTROLLER>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScripth = Player.GetComponent<PlayerUpdate>();
        GameController.UpdateWorldLevel(WorldLevel);
        PlayerScripth.setWorldLevelAmount(WorldLevel);


        Audio_Controller = GameObject.FindGameObjectWithTag("AUDIO_CONTROLLER");
        Audio_Scripth = Audio_Controller.GetComponent<AudIoController>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerGemUpdate();
        WorldrGemUpdate();
        
    }

    void WorldrGemUpdate()
    { 
        if(WorldLevel >= 6 && !END_GAME_ACTIVE)
        {
            END_GAME_ACTIVE = true;
            GameController.setBossActive();
            Audio_Scripth.FadeOutGameMusic();
            Audio_Scripth.FadeInBossMusic();
        }
        if(playerLevel == 5)
        {
            TriggerWorldUpgrade = true;
            GameController.UpdateWorldLevel(WorldLevel+1);
            if(!WorldGem_sound.isPlaying)
            {
                WorldGem_sound.Play();
            }
            StartCoroutine(WaitWorldUpdate());

            playerLevel = 0;
        }
        if(TriggerWorldUpgrade)
        {
            for (int i = 0; i < Gems_player.Length; i++)
            {
                GemRotateScripth =  Gems_player[i].GetComponent<RotateAroundGivenPoint>();
                GemRotateScripth.Speed += gemSpeedIncrease;
            }

            if(GemRotateScripth.Speed > gemSpeedStop)
            {
                TriggerWorldUpgrade = false;
                TriggerWorldGemCombine = true;
            }
        }
        else if(TriggerWorldGemCombine)
        {
            updateCountUpgarde++;
            for (int i = 0; i < Gem_particals.Length; i++)
            {
                new_Gem_particals = Instantiate(Gem_particals[i], Vector3.zero, Quaternion.identity);
                new_Gem_particals.transform.position = Gems_player[i].transform.position;
                new_Gem_particals.transform.parent = GEM_CONTAINER.transform;
                new_rb = new_Gem_particals.GetComponent<Rigidbody2D>();
                new_rb.velocity = Random.onUnitSphere * Force;
            }

            if(updateCountUpgarde > updateCountUpgardeLength)
            {
                updateCountUpgarde = 0;
                TriggerWorldGemCombine = false;
                TriggerWorldGemEnd = true;

                for (int i = 0; i < Gems_player.Length; i++)
                {
                    Gem_player_Renderer = Gems_player[i].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = false;
                    Gem_playerLight.enabled = false;
                }

            }
        }
        else if(TriggerWorldGemEnd)
        {
            updateCountEnd++;
            
            for (int i = 0; i < Gem_particals.Length; i++)
            {
                new_Gem_particals = Instantiate(Gem_particals[i], Vector3.zero, Quaternion.identity);
                new_Gem_particals.transform.position = Gems_player[i].transform.position;
                new_Gem_particals.transform.parent = GEM_CONTAINER.transform;
                new_rb = new_Gem_particals.GetComponent<Rigidbody2D>();
                new_rb.velocity = Random.onUnitSphere * Force*3;
            }

            if(updateCountEnd > updateCountEndLength)
            {            
                TriggerWorldGemEnd = false;
                updateCountEnd = 0;

                for (int i = 0; i < Gems_player.Length; i++)
                {                    
                    GemRotateScripth = Gems_player[i].GetComponent<RotateAroundGivenPoint>();
                    GemRotateScripth.Speed = GemRotateScripth.StartSpeed;
                }
            }
        }
    }

    void PlayerGemUpdate()
    {
        Gems_count = GameObject.FindGameObjectsWithTag("Gem");
        
        if(Gems_count.Length >= 5)
        {
            StartCoroutine(Wait());
        }        
        else
        {
            
          StartEndGems = false;  
        }   
          
        if(StartEndGems)
        {
            updateCount++;

            if(updateCount < framesTillStop )
            {   
                for (int i = 0; i < Gem_particals.Length; i++)
                {
                    new_Gem_particals = Instantiate(Gem_particals[i], Vector3.zero, Quaternion.identity);
                    new_Gem_particals.transform.position = Gem_particals[i].transform.position;
                    new_Gem_particals.transform.parent = GEM_CONTAINER.transform;
                    new_rb = new_Gem_particals.GetComponent<Rigidbody2D>();
                    new_rb.velocity = Random.onUnitSphere * Force; 
                }
            }
            else    
            {
                for (int i = 0; i < Gems.Length; i++)
                {
                    GemSpriteRenderer = Gems[i].GetComponent<SpriteRenderer>();
                    GemDrawInFireFlys = Gems[i].GetComponent<DrawInFireFlys>();
                    GemDrawInFireFlys.stop = false;
                    GemSpriteRenderer.enabled = false;
                    Gems[i].SetActive(false);

                }
                PlayerScripth.ResetLevel();
                StartEndGems = false;
                updateCount = 0;

                playerLevel++;
                Gem_sound.Play();
                switch (playerLevel)
                {
                    
                    default:
                    case 0:

                    break;
                
                    case 1:
                    Gem_player_Renderer = Gems_player[0].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[0].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = true;
                    Gem_playerLight.enabled = true;
                    break;
       
                    case 2:
                    Gem_player_Renderer = Gems_player[1].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[1].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = true;
                    Gem_playerLight.enabled = true;
                    break;

                    case 3:
                    Gem_player_Renderer = Gems_player[2].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[2].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = true;
                    Gem_playerLight.enabled = true;
                    break;
                
                    case 4:
                    Gem_player_Renderer = Gems_player[3].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[3].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = true;
                    Gem_playerLight.enabled = true;
                    break;

                    case 5:
                    Gem_player_Renderer = Gems_player[4].GetComponent<MeshRenderer>();
                    Gem_playerLight = Gems_player[4].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                    Gem_player_Renderer.enabled = true;
                    Gem_playerLight.enabled = true;
                    break;
                }

            } 
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        StartEndGems = true;

    }
    IEnumerator WaitWorldUpdate()
    {
        yield return new WaitForSeconds(2);
        Gems_World_indicator[WorldLevel].SetActive(true);
        PlayerScripth.setWorldLevelAmount(WorldLevel);
        WorldLevel++;

    }

    public int GetWorldLevel()
    {
        if(WorldLevel > 6)
        {
            WorldLevel = 6;
        }
        return WorldLevel;
    }
}
