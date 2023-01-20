using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreWords : MonoBehaviour
{

    private GameObject[] Letters =  new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        Letters = GameObject.FindGameObjectsWithTag("Word");
        for (int i = 0; i < Letters.Length; i++)
        {
            Physics2D.IgnoreCollision(Letters[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
