using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{
    private GameObject Player;
    private Vector3 PlayerPos;
    private Vector3 MoveTo;


    public float m_SpeedMin = 1f;
    public float m_SpeedMax = 5.0f;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = Vector2.MoveTowards(transform.position, PlayerPos, Random.Range(0.1f, 2.0f));     

        dist = Vector3.Distance(PlayerPos, transform.position);
        if(dist <= 10.0f)
        {
            StartCoroutine(waitForDeath());
        }
    }

    IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);  
    }
}
