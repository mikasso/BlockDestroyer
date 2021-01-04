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

    /// Not needed, delete later..
    void Start()
      {
          for (int i=0; i<rows; i++)
          {
              for(int j=0; j<blocksInRow; j++)
              {
                  Instantiate(block, new Vector3(leftX+ blockSize*j, topY - blockSize*i, 0), Quaternion.identity);//4x6 klocow
              }
          }
      }
    

    //TODO
    /// <summary>
    /// <returns>Return true if new generated line is above min level else false which means losing a game .</returns>
    /// </summary>
    internal bool GenerateNewLineOfBlocks()
    {
        moveAllBlocksLevelsDown(level: 1);
        //TODO
        return true;
    }

    private void moveAllBlocksLevelsDown(int level)
    {
       
    }

    internal void SaveBlocks()
    {
       
    }
    internal void LoadBlocks()
    {
       
    }
    internal void ForgotBlocks()
    {
        
    }
}
