using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionParticles : MonoBehaviour
{
    public GameObject Particles;
    private AudioSource audioData;
    public GameObject Camera;
    private Shake CameraShake;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        CameraShake= Camera.GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Particles.SetActive(true);
            if(!audioData.isPlaying)
            {
             audioData.Play(0);
             CameraShake.TriggerShake(0.2f, 0.01f);
            }
            StartCoroutine(WaitToEndParticles());
        }
    
    }

    IEnumerator WaitToEndParticles()
    {
        yield return new WaitForSeconds(2);        
        audioData.Stop();
        Particles.SetActive(false); 
    }
}
