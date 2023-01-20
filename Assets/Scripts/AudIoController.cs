using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudIoController : MonoBehaviour
{   
    public GameObject GameBackground;
    private AudioSource GameBackground_music;

    public GameObject BossBackground;
    private AudioSource BossBackground_music;


    public float MusicFadeSpeed = 0.01f;

    public float gameMusicVolume = 0.1f;
    public float BossMusicVolume = 0.1f;

    private bool GameFadeIn = false;
    private bool BossFadeIn = false;

    private bool GameFadeOut = false;
    private bool BossFadeOut = false;
    // Start is called before the first frame update
    void Start()
    {
        GameBackground_music = GameBackground.GetComponent<AudioSource>();
        BossBackground_music = BossBackground.GetComponent<AudioSource>();
        FadeInGameMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameFadeIn == true)
        {
            if(GameBackground_music.volume < gameMusicVolume)
            {
                GameBackground_music.volume  += MusicFadeSpeed;   
            }
            else
            {
                GameFadeIn = false;
            }
        }
        if(BossFadeIn == true)
        {
            if(BossBackground_music.volume < BossMusicVolume)
            {
                BossBackground_music.volume  += MusicFadeSpeed;   
            }
            else
            {
                BossFadeIn = false;
            }
        }



        if(GameFadeOut == true)
        {
            if(GameBackground_music.volume > 0.0f)
            {
                GameBackground_music.volume  -= MusicFadeSpeed;   
            }
            else
            {
                StopGameMusic();
                GameFadeOut = false;
            }
        }
        if(BossFadeOut == true)
        {
            if(BossBackground_music.volume > 0.0f)
            {
                BossBackground_music.volume  -= MusicFadeSpeed;   
            }
            else
            {
                StopBossMusic();
                BossFadeOut = false;
            }
        }
    }


    public void StopGameMusic()
    {
        GameBackground_music.Stop();
    }

    public void StartGameMusic()
    {
        GameBackground_music.Play();
    }

    public void StopBossMusic()
    {
        BossBackground_music.Stop();
    }

    public void StartBossMusic()
    {
        BossBackground_music.Play();        
    }
    
    public void FadeInGameMusic()
    {
        StartGameMusic();
        GameFadeIn = true;
    }
    public void FadeInBossMusic()
    {   
        StartBossMusic();
        BossFadeIn = true;
    }


    public void FadeOutGameMusic()
    {
        GameFadeOut = true;
    }
    public void FadeOutBossMusic()
    {           
        BossFadeOut = true;
    }
}
