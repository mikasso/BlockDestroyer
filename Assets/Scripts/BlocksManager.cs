using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    // Start is called before the first frame updateffd
    public GameObject block;
    public ParticleSystem ps;
    public int amount;
    public const int blocksInRow = 6;
    public const int rows = 4;
    public const float leftX = 0.45f;
    public const float topY = 10.0f;
    public const float blockSize = 1.0f;

    void Start()
    {
        ps = (ParticleSystem)Resources.Load("Prefabs/Particle System", typeof(ParticleSystem));
        Instantiate(ps, new Vector3(3.0f,5.0f,0), Quaternion.identity);
        ps.Stop();
        for (int i=0; i<1; i++)
        {
            for(int j=0; j<1; j++)
            {
                Instantiate(block, new Vector3(leftX+ blockSize*j+2.5f, topY - blockSize*i-6.0f, 0), Quaternion.identity);//4x6 klocow
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
