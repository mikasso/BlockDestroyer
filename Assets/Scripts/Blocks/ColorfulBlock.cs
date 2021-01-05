using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulBlock : AbstractBlock
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        setRandomColor();
    }

    public void setRandomColor()
    {

    }
}
