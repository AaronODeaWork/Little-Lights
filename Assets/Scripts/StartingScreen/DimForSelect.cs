using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimForSelect : MonoBehaviour
{
    private GameObject Player;
    public GameObject LightPartical;

    public GameObject Fade;

    private float Transparent = 0.0f;
    public float TransparentSpeed = 0.1f;
    public float LightMult = 10.0f;

    private SpriteRenderer sprite;

    private UnityEngine.Experimental.Rendering.Universal.Light2D LampLight;
    public float LampHealth = 50;
    private float CurrentLampHealth;

    public float playerHealthIncreasePS = 0.5f;

    private  bool LampAlive = true;
    private  bool LampAliveFinsihed = false;

    private bool PlayerInArea = false;

    public string SceanName = "Game";

    private GameObject Audio_Controller;
    private AudioControllerMenus Audio_Scripth;

    private bool AbsorbingStart = true;

    // Start is called before the first frame update
    void Start()
    {

        Audio_Controller = GameObject.FindGameObjectWithTag("AUDIO_CONTROLLER");
        Audio_Scripth = Audio_Controller.GetComponent<AudioControllerMenus>();

        CurrentLampHealth = LampHealth+1;
        LampLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        sprite = Fade.GetComponent<SpriteRenderer>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerInArea)
        {
            
            if(CurrentLampHealth < (LampHealth+1))
            {
                if(!AbsorbingStart)
                {
                    Audio_Scripth.StopAbsorb();
                    AbsorbingStart = true;
                }
                CurrentLampHealth += playerHealthIncreasePS;
                LampLight.intensity =  (CurrentLampHealth/LampHealth)*LightMult;
            }
        }

        if(!LampAlive)
        {
            if(!LampAliveFinsihed)
            {
                Audio_Scripth.PlaySelected();
                LampAliveFinsihed = true;
            }
            
            Transparent += TransparentSpeed;
            sprite.color = new Color (0, 0, 0, Transparent); 

            if(Transparent >= 1.5f)
            {
                SceneManager.LoadScene (sceneName:SceanName.ToString());
            }
            
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerInArea = true;
            if(CurrentLampHealth>1.0f)
            {
                if(AbsorbingStart)
                {
                    Audio_Scripth.PlayAbsorb();
                    AbsorbingStart = false;
                }
                CurrentLampHealth -= playerHealthIncreasePS;

                GameObject new_LightPartical = Instantiate(LightPartical, Vector3.zero, Quaternion.identity);
                new_LightPartical.transform.position = new Vector3(transform.position.x + Random.Range(-1,1),transform.position.y+ Random.Range(-1,1),0);
            }
            else
            {
                LampAlive = false;
            }
            LampLight.intensity =  (CurrentLampHealth/LampHealth)*LightMult;
        }
        

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
           PlayerInArea = false;
           
        }
        

    }
}
