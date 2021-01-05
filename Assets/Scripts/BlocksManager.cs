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

    private int hardnessLevel = 1;
    //TODO
    /// <summary>
    /// <returns>Return true if new generated line is above min level else false which means losing a game .</returns>
    /// </summary>
    internal bool GenerateNewLineOfBlocks()
    {
        hardnessLevel++;
        generateLine(-1);
        return moveAllBlocksLevelsDown(level: 1);
        //TODO
    }

    private void randomizeBlock()
    {
        AbstractBlock abstractBlock = block.GetComponent<AbstractBlock>();
        abstractBlock.life = UnityEngine.Random.Range(1, hardnessLevel);
        SpriteRenderer rend = block.GetComponent<SpriteRenderer>();
        rend.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.75f, 1f);
    }

    private void generateLine(int level)
    {
        for (int j = 0; j < blocksInRow; j++)
        {
            randomizeBlock();
            Instantiate(block, new Vector3(leftX + blockSize * j, topY - blockSize * level, 0), Quaternion.identity);//4x6 klocow
        }
    }

    private bool moveAllBlocksLevelsDown(int level)
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject blockObject in arr)
        {
            blockObject.transform.Translate(new Vector3(0, -1.0f));
        }
        return true;
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
