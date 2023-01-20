using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHand : MonoBehaviour
{
    private GameObject Player;
    public GameObject GroundHandSpawn;

    private SpawnGemParticals GemScripth;
    public float UpdateOnFrame_PlayerPos = 50;

    private float updateCount = 0;
    public float Speed = 4.0f;

    private GameObject ENEMY_CONTAINER;
    private  GAME_CONTROLLER GameController;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ENEMY_CONTAINER= GameObject.FindGameObjectWithTag("ENEMY_CONTAINER");
        GameController = GameObject.FindGameObjectWithTag("GAME_CONTROLLER").GetComponent<GAME_CONTROLLER>();
        GemScripth = GameController.GetComponent<SpawnGemParticals>();
    }  

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        { 
            if(GemScripth.WorldLevel >=2)
            {
                updateCount++;
                if(updateCount > UpdateOnFrame_PlayerPos)
                {   
                    updateCount = 0;
                    GameObject new_Hand = Instantiate(GroundHandSpawn, Vector3.zero, Quaternion.identity);
                    new_Hand.transform.position = new Vector3(Player.transform.position.x, GroundHandSpawn.transform.position.y,0.0f);
                    new_Hand.transform.parent = ENEMY_CONTAINER.transform;

                }
            }
    
        }
    }



}