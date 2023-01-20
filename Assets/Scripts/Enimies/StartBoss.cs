using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{

    private Vector3 EndPosition;
    public GameObject Nextlevel;
    // Start is called before the first frame update
    void Start()
    {
        EndPosition = new Vector3(18.22f,7.35f,0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, EndPosition, 0.1f);
        if( transform.position == EndPosition)
        {
            Destroy(gameObject);
            Nextlevel.SetActive(true);
        }
    }
}
