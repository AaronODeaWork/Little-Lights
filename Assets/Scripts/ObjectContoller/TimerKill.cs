using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerKill : MonoBehaviour
{

    public int TimeToKill = 10;
    private int FrameCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FrameCount++;

        if(FrameCount > TimeToKill)
        {
            Destroy(gameObject);
        }
        
    }
}
