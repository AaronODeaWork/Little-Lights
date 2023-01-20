using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDimLight : MonoBehaviour
{
    private GameObject Player;
    public GameObject LightPartical;

    private UnityEngine.Experimental.Rendering.Universal.Light2D Light;

    private float lightHealth = 100;

    private GameObject DrawLifeObj;
    private AudioSource DrawLifeSound;

    // Start is called before the first frame update
    void Start()
    {

        DrawLifeObj =  GameObject.FindGameObjectWithTag("EnemyAbsorbSound");
        DrawLifeSound = DrawLifeObj.GetComponent<AudioSource>();

        Light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(lightHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

            if(!DrawLifeSound.isPlaying)
            {
                DrawLifeSound.Play();
            }
            lightHealth--;
            GameObject new_Partical = Instantiate(LightPartical, Vector3.zero, Quaternion.identity);
            new_Partical.transform.position = new Vector3(transform.position.x + Random.Range(-1,1),transform.position.y+ Random.Range(-1,1),0);

            Light.intensity =  lightHealth/10;
        }
        

    }
}
