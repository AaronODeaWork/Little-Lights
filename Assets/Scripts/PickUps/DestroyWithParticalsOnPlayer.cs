using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithParticalsOnPlayer : MonoBehaviour
{
    public ParticleSystem EndParticals;
    private ParticleSystem new_Particals;
    private CircleCollider2D CColider;
    private TrailRenderer trail;
    private ParticleSystem particals;
    private SpriteRenderer Sprite;
    private UnityEngine.Experimental.Rendering.Universal.Light2D Light;

    private GameObject Camera;
    private Shake CameraShake;

    public AudioSource FireBallDead;

    // Start is called before the first frame update
    void Start()
    {
        CColider = gameObject.GetComponent<CircleCollider2D>();
        trail = gameObject.GetComponent<TrailRenderer>();
        particals = gameObject.GetComponent<ParticleSystem>();
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        Light = gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        Camera = GameObject.FindGameObjectWithTag("Camera");
        CameraShake= Camera.GetComponent<Shake>();

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        { 
            new_Particals = Instantiate(EndParticals, Vector3.zero, Quaternion.identity);
            new_Particals.transform.position = transform.position;
            CColider.enabled= false;
            trail.enabled= false;
            Sprite.enabled = false;
            Light.enabled = false;
            CameraShake.TriggerShake(0.2f, 0.01f);
            particals.Stop();
            FireBallDead.Play();
            
        }
    }
}
