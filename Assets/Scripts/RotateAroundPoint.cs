using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    private GameObject GAME_CONTROLLER;
    private GAME_CONTROLLER GAME_CONTROLLER_SCRIPTH;
    // Start is called before the first frame update
    void Start()
    {
        GAME_CONTROLLER = GameObject.FindGameObjectWithTag("GAME_CONTROLLER");
        GAME_CONTROLLER_SCRIPTH = GAME_CONTROLLER.GetComponent<GAME_CONTROLLER>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 point = new Vector3(0,-30,0);
        Vector3 axis = new Vector3(0,0,1);
        //transform.RotateAround(point, axis, Time.deltaTime * -0.5f);
        transform.RotateAround(point, axis, Time.deltaTime * -GAME_CONTROLLER_SCRIPTH.m_OrbitSpeed);

    }
}
