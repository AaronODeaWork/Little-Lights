using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerMenus : MonoBehaviour
{
    public GameObject menu;
    private AudioSource menu_music;

    public GameObject Selected;
    private AudioSource Selected_Sound;

    public GameObject Absorb;
    private AudioSource Absorb_Sound;

    private bool fadeIn = true;
    private bool fadeOut = false;


    public float MusicFadeSpeed = 0.01f;

    public float menuVolume = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        menu_music = menu.GetComponent<AudioSource>();
        Selected_Sound = Selected.GetComponent<AudioSource>();
        Absorb_Sound = Absorb.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn)
        {  
            if(menu_music.volume < menuVolume)
            {
                menu_music.volume  +=MusicFadeSpeed;
            }
            else
            {
                fadeIn = false;
            }
        }
        else if(fadeOut)
        {
            if(menu_music.volume > 0.0f)
            {
                menu_music.volume  -=MusicFadeSpeed;
            }
            else
            {
                fadeOut = false;
            }
        }
    }




    public void FadeOut()
    {
       fadeOut = true; 
    }

    public void PlaySelected()
    {
        Selected_Sound.Play();
    }
    public void PlayAbsorb()
    {
        Absorb_Sound.Play();
    }
    public void StopAbsorb()
    {
        Absorb_Sound.Stop();
    }
}
