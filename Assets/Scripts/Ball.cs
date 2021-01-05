using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 Velocity
    {
        get {
            rb = GetComponent<Rigidbody2D>();
            return rb.velocity;
        }
        set
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = value;
        }
    }
    

}
