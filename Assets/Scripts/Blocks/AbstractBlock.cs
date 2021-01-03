using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBlock : MonoBehaviour
{
    public int life;
    public PlayerManager player;
    public GameObject blast;
    private int hitValue = 1;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        player.IncreaseScore(hitValue);
        life--;
        if (life <= 0)
        {
            Instantiate(blast, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
