using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInFireFlys : MonoBehaviour
{

    private GameObject[] PickUp_Orbs = new GameObject[9999];
    private GameObject Player;
    private PlayerUpdate PlayerScripth;
    private SpriteRenderer spriteRenderer;
    private int LevelUpAmount;

    public float RunningCount = 0;
    public bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScripth = Player.GetComponent<PlayerUpdate>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            PickUp_Orbs = GameObject.FindGameObjectsWithTag("PickUp_ORBS");
      
            for (int i = 0; i < PickUp_Orbs.Length; i++)
            {
                Rigidbody2D new_FireFlyRB = PickUp_Orbs[i].GetComponent<Rigidbody2D>();
                new_FireFlyRB.velocity = Random.onUnitSphere * 10;
                PickUp_Orbs[i].transform.position = Vector2.MoveTowards(PickUp_Orbs[i].transform.position, transform.position, Random.Range(0.1f, 2.0f));     
                float Dist = Vector3.Distance(PickUp_Orbs[i].transform.position, transform.position);
                if(Dist < 0.5f )
                {   
                    RunningCount++;
                    Destroy(PickUp_Orbs[i]);                    
                }       
            }
        }
        
        if(RunningCount > LevelUpAmount)
        {
            stop= true;
            spriteRenderer.enabled = true; 
            PlayerScripth.m_GemPause = false;
            RunningCount = 0;
        }        
    }
    
    public void setLevelUpAmount(int LvAmount)
    {
        LevelUpAmount = LvAmount;
    }
}
