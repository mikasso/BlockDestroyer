﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBlock : MonoBehaviour
{
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        life--;
        if (life <= 0)
            Destroy(gameObject);
    }
}
