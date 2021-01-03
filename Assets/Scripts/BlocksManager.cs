using System;
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
    public  float leftX = 0.45f;
    public float topY = 10.0f;
    public float blockSize = 1.0f;

    void Start()
    {
        //ps = (ParticleSystem)Resources.Load("Prefabs/Particle System", typeof(ParticleSystem));
        //Instantiate(ps, new Vector3(3.0f,5.0f,0), Quaternion.identity);
        //ps.Stop();
        for (int i=0; i<rows; i++)
        {
            for(int j=0; j<blocksInRow; j++)
            {
                Instantiate(block, new Vector3(leftX+ blockSize*j+2.5f, topY - blockSize*i-6.0f, 0), Quaternion.identity);//4x6 klocow
            }
        }
    }

    internal bool GenerateNewLineOfBlocks()
    {
        throw new NotImplementedException();
    }


}
