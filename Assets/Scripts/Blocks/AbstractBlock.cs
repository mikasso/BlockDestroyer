using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractBlock : MonoBehaviour
{
    public int life;
    public PlayerManager player;
    public Text mytext;
    public ParticleSystem ps;

    private int hitValue = 1;
    // Start is called before the first frame update
    protected virtual void Start()
    {
       // ps.Pause();
        mytext.text = life.ToString();
        mytext.fontSize = 23;
        RectTransform rt = mytext.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(transform.position.x*179-525.5f, transform.position.y*179-926.0f, 0);
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        player.IncreaseScore(hitValue);
        mytext.text = life.ToString();
        life--;
       //             ps.Emit(100);
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
