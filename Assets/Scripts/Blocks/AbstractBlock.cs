using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class AbstractBlock : MonoBehaviour
{
    public int life;
    public PlayerManager player;
    public Canvas canvas;
    public GameObject particleObject;
    
    protected SpriteRenderer rend;
    protected Text textInfo;
    protected int hitValue = 1;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // ps.Pause();
        canvas = gameObject.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        textInfo = canvas.GetComponentInChildren<Text>();
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerManager>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        updateLifeScore();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        player.IncreaseScore(hitValue);
        life -= ball.damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        else
            updateLifeScore();
        
        Instantiate(particleObject, transform.position, Quaternion.identity);
    }

    private void updateLifeScore()
    {
        textInfo.text = life.ToString();
    }
	
}
