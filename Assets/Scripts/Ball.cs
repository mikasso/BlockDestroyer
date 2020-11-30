using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
         rb.velocity = new Vector2(0.0f,velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
