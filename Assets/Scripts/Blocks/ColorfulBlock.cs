﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulBlock : AbstractBlock
{
    // Start is called before the first frame update
    public GameObject particleObject;
    override protected void Start()
    {
        base.Start();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        ParticleSystem.MainModule newMain = particleObject.GetComponentInChildren<ParticleSystem>().main;
        newMain.startColor = GetComponent<SpriteRenderer>().color;
        Instantiate(particleObject, collision.transform.position, Quaternion.identity);
        setRandomColor();
    }

    public void setRandomColor()
    {
        GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.75f, 1f);
    }
}
