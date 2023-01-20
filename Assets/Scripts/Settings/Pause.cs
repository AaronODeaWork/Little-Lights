using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool PauseGameBool = false;
    public GameObject PauseObject;

    private float Transparent = 0.0f;
    private float TransparentMax = 1.2f;

    public float TransparentSpeed = 0.1f;

    private bool Transtion = false;
    private bool ButtonPause = false;

    private SpriteRenderer sprite;


    private int count = 0;

    private int countMax = 100;
    // Start is called before the first frame update
    void Start()
    {
        sprite =  PauseObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape) && PauseGameBool && !ButtonPause)//pause
        {   
            ButtonPause = true;
            Transtion = true;
            PauseGameBool = false;
        }
        else if (Input.GetKey(KeyCode.Escape) && !PauseGameBool && !ButtonPause)//resume
        {
            ButtonPause = true;
            PauseObject.SetActive(true);
            Transtion = true;
            PauseGameBool = true;
        }
        
        

        if(Transtion)
        {
            if(PauseGameBool)
            { 
                
                Transparent += TransparentSpeed;
                sprite.color = new Color (1,1, 1, Transparent); 

                if(Transparent >= TransparentMax)
                {
                    Transtion = false;
                    PauseGame();
                }
            }
            else if(!PauseGameBool)
            {
                ResumeGame();
                Transparent -= TransparentSpeed;
                sprite.color = new Color (1, 1, 1, Transparent); 

                if(Transparent <= 0.0f)
                {    
                    PauseObject.SetActive(false);
                    Transtion = false;
                }
            }
        }
    
        if(ButtonPause)
        {
            count++;
            if(count >= countMax )
            {
                count = 0;
                ButtonPause = false;
            }
        }
    }

    void PauseGame ()
    {
        Time.timeScale = 0;
    }

    void ResumeGame ()
    {
        Time.timeScale = 1;
    }



}
