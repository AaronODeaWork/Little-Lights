using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLayer : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = Random.Range(-1, 1);
        if(sprite.sortingOrder == 0)
        {
          sprite.sortingOrder = 1;
        }
    }

}
