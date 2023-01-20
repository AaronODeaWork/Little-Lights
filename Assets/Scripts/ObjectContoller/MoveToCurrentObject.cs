using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCurrentObject : MonoBehaviour
{
    private GameObject Object;
    public float Maxpeed = 50.0f;
    public float speedIncrease = 5.0f;
    public float speed = 1.0f;
    private bool objectSet = false;

    private float Dist = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(objectSet)
        {
            speed += speedIncrease;
            if(speed >= Maxpeed)
            {
                speed = Maxpeed;
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Object.transform.position, step);
            

            Dist = Vector3.Distance(Object.transform.position, transform.position);
            if(Dist < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setObject(GameObject  GivenObject)
    {
        Object = GivenObject;
        objectSet = true;
    }
}
