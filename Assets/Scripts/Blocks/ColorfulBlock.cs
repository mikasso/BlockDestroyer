using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulBlock : AbstractBlock
{
    private ParticlePoll particlePoll;
    private ColorsPoll colorsPoll;
    private GameObject GameController;
    public override void Awake()
    {
        Debug.Log("Awake colorful");
        base.Awake();
        GameController = GameObject.Find("GameController");
        particlePoll = GameController.GetComponent<ParticlePoll>();
        colorsPoll = GameController.GetComponent<ColorsPoll>();
    }
    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        ChangeColorToRandom();
        particlePoll.PlayOneShot(collision.transform.position);
    }

    internal void ChangeColorToRandom()
    {
        GetComponent<SpriteRenderer>().color = colorsPoll.GetRandomColor();
    }    
}
