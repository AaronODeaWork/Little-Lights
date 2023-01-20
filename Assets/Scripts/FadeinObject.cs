using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeinObject : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float Transparent = 1.2f;
    public float TransparentSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        sprite =  gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Transparent -= TransparentSpeed;
        sprite.color = new Color (0, 0, 0, Transparent); 

        if(Transparent <= 0.0f)
        {
            Destroy(gameObject);
        }
            
    }
}
