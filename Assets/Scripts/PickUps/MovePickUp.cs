using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePickUp : MonoBehaviour
{
    public float m_DeletePosition = -70.0f;

    public float m_SpeedMin = 1f;
    public float m_SpeedMax = 5.0f;

    private float m_Speed = 0.0f;

    public GameObject[] m_UI_fireFly = new GameObject[6];

    private AudioSource audioData;
    private GameObject SCORE_CONTAINER;
    private GameObject GAME_CONTROLLER;

    private SpawnGemParticals WorldLevelScripth;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        m_Speed =  Random.Range(m_SpeedMin, m_SpeedMax);

        GAME_CONTROLLER= GameObject.FindGameObjectWithTag("GAME_CONTROLLER");
        WorldLevelScripth = GAME_CONTROLLER.GetComponent<SpawnGemParticals>();
        SCORE_CONTAINER= GameObject.FindGameObjectWithTag("SCORE_CONTAINER");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( -Time.deltaTime*m_Speed,0, 0, Space.World);
    }

    void OnCollisionEnter2D(Collision2D other)
    {           
        if(other.gameObject.tag == "LEFTSCREEN")
        {
            Destroy(gameObject);  
        }
        if(other.gameObject.tag == "Player")
        {            
            GameObject new_UIFly = Instantiate(m_UI_fireFly[WorldLevelScripth.GetWorldLevel()], Vector3.zero, Quaternion.identity);
            new_UIFly.transform.parent = SCORE_CONTAINER.transform;
            new_UIFly.transform.position = new Vector3(30,-15,0);
            audioData.Play(0);
            StartCoroutine(WaitOnDeath());
        }
    }

    IEnumerator WaitOnDeath()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);  
    }

}
