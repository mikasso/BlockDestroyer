using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class AbstractBlock : MonoBehaviour
{
    public int Life {
        get
        {
            return life;
        }
        set
        {
            life = value;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
            else
                updateLifeScore();
        }
    }

    protected Text textInfo;
    protected int hitValue = 1;

    private int life = 1;
    private PlayerManager player;
    private Canvas canvas;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        // ps.Pause();
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        canvas = gameObject.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        textInfo = canvas.GetComponentInChildren<Text>();
        updateLifeScore();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Life--;
        player.IncreaseScore(hitValue);
    }

    private void updateLifeScore()
    {
        Debug.Log(textInfo);
        textInfo.text = Life.ToString();
    }
	
}
