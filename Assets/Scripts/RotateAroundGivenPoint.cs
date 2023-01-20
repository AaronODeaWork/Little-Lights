using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundGivenPoint : MonoBehaviour
{
    public GameObject GivenObject;

    private Vector3 point;
    private Vector3 axis = new Vector3(1,1,1);
    public float Speed = 100.0f;
    public float StartSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        point = GivenObject.transform.position;
        
        transform.RotateAround(point, axis, Time.deltaTime * -Speed);

    }
}
