using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLightToEnemy : MonoBehaviour
{
    private GameObject Player;
    public GameObject LightPartical;

    private PlayerUpdate playerScript;
    private MoveToCurrentObject particalScripth;

    private GameObject DrawLifeObj;
    private AudioSource DrawLifeSound;

    // Start is called before the first frame update
    void Start()
    {
        DrawLifeObj =  GameObject.FindGameObjectWithTag("EnemyAbsorbSound");
        DrawLifeSound = DrawLifeObj.GetComponent<AudioSource>();

        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<PlayerUpdate>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && playerScript.Health < 100.0f && playerScript.getAlive())
        {
            if(!DrawLifeSound.isPlaying)
            {
                DrawLifeSound.Play();
            }
            GameObject new_LightPartical = Instantiate(LightPartical, Vector3.zero, Quaternion.identity);
            new_LightPartical.transform.position = new Vector3(Player.transform.position.x + Random.Range(-1,1),transform.position.y+ Random.Range(-1,1),0);
            particalScripth  = new_LightPartical.GetComponent<MoveToCurrentObject>();
            particalScripth.setObject(this.gameObject);
        }
    }

}
