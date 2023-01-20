using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGemController : MonoBehaviour
{
    public GameObject[] BushOne = new GameObject[4];
    public GameObject[] MushRoom = new GameObject[6];
    public GameObject[] Pumpkin = new GameObject[3];
    public GameObject[] SunFlower = new GameObject[2];
    public GameObject ButterFly;

    private GameObject[] Gems = new GameObject[5];
    private SpawnGemParticals GemScripths;

    private int randomChoice = 0;
    public float m_BushSpawnChancePercent = 10;
    public float m_MushroomSpawnChancePercent = 10;
    public float m_PumpkinSpawnChancePercent = 10;
    public float m_SunflowerSpawnChancePercent = 10;
    public float m_ButterFlySpawnChancePercent = 10;

    private GameObject BACKGROUND_CONTAINER;

    // Start is called before the first frame update
    void Start()
    {
        GemScripths = GameObject.FindGameObjectWithTag("GAME_CONTROLLER").GetComponent<SpawnGemParticals>();
        BACKGROUND_CONTAINER = GameObject.FindGameObjectWithTag("GROUND_CONTAINER");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Spawns certain world elements based on world level 
        switch (GemScripths.WorldLevel)
        {            
            default:
            case 0:
            break;

            case 1:
                SpawnButterFly();
            break;

            case 2:
                SpawnButterFly();
                SpawnSunFlower();
            break;

            case 3:
                SpawnButterFly();
                SpawnSunFlower();
                SpawnPumpkin();
            break;

            case 4:
                SpawnButterFly();
                SpawnSunFlower();
                SpawnPumpkin();
                SpawnMushRoom();
            break;

            case 5:
                SpawnButterFly();
                SpawnSunFlower();
                SpawnPumpkin();
                SpawnMushRoom();
                SpawnBush();
            break;

        }        
    }

    void SpawnBush()
    {   
        if( Random.Range(1, 10000) <= m_BushSpawnChancePercent)
        {    
            randomChoice = Random.Range(0,BushOne.Length);

            GameObject new_BushOne = Instantiate(BushOne[randomChoice], Vector3.zero, Quaternion.identity);
            new_BushOne.transform.position = BushOne[randomChoice].transform.position;
            new_BushOne.transform.parent = BACKGROUND_CONTAINER.transform;
        }
    }
    void SpawnMushRoom()
    {

        if( Random.Range(1, 10000) <= m_MushroomSpawnChancePercent)
        {
            randomChoice = Random.Range(0,MushRoom.Length);

            GameObject new_MushRoom = Instantiate(MushRoom[randomChoice], Vector3.zero, Quaternion.identity);
            new_MushRoom.transform.position = MushRoom[randomChoice].transform.position;
            new_MushRoom.transform.parent = BACKGROUND_CONTAINER.transform;
        }
    }
    void SpawnPumpkin()
    {
        if( Random.Range(1, 10000) <= m_PumpkinSpawnChancePercent)
        {
            randomChoice = Random.Range(0,Pumpkin.Length);

            GameObject new_Pumpkin = Instantiate(Pumpkin[randomChoice], Vector3.zero, Quaternion.identity);
            new_Pumpkin.transform.position = Pumpkin[randomChoice].transform.position;
            new_Pumpkin.transform.parent = BACKGROUND_CONTAINER.transform;
        }
    }
    void SpawnSunFlower()
    {
        if( Random.Range(1, 10000) <= m_SunflowerSpawnChancePercent)
        {
            randomChoice = Random.Range(0,SunFlower.Length);

            GameObject new_SunFlower = Instantiate(SunFlower[randomChoice], Vector3.zero, Quaternion.identity);
            new_SunFlower.transform.position = SunFlower[randomChoice].transform.position;
            new_SunFlower.transform.parent = BACKGROUND_CONTAINER.transform;
        }
    }
    void SpawnButterFly()
    {
        if( Random.Range(1, 10000) <= m_ButterFlySpawnChancePercent)
        {
            GameObject new_ButterFly = Instantiate(ButterFly, Vector3.zero, Quaternion.identity);
            new_ButterFly.transform.position = new Vector3(ButterFly.transform.position.x,ButterFly.transform.position.y+ Random.Range(-5,1),ButterFly.transform.position.z);
            new_ButterFly.transform.parent = BACKGROUND_CONTAINER.transform;
        }
    }

}
