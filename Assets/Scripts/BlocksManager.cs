using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BlocksManager : MonoBehaviour
{
    public GameObject block;
    public int amount;
    public const int blocksInRow = 6;
    public const int rows = 4;
    public  float leftX = 0.45f;
    public float topY = 10.0f;
    public float blockSize = 1.0f;

    private int hardnessLevel = 1;
    /// <summary>
    /// <returns>Return true if new generated line is above min level else false which means losing a game .</returns>
    /// </summary>
    internal bool GenerateNewLineOfBlocks()
    {
        hardnessLevel++;
        generateLine(-1);
        return moveAllBlocksLevelsDown(level: 1);
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
       BinaryFormatter bf = new BinaryFormatter();
	   FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
	   GameObject[] arr = GameObject.FindGameObjectsWithTag("Block");
	   BlockData bd = new BlockData(arr.Length);
	   int i=0;
	   foreach(GameObject blockObject in arr)
	   {
		   SpriteRenderer rend = blockObject.GetComponent<SpriteRenderer>();
		   bd.color[i,0] = rend.color[0];
		   bd.color[i,1] = rend.color[1];
		   bd.color[i,2] = rend.color[2];
		   bd.color[i,3] = rend.color[3];
		   bd.life[i] = blockObject.GetComponent<AbstractBlock>().life;
		   bd.position[i,0] = blockObject.transform.position.x;
		   bd.position[i,1] = blockObject.transform.position.y;
		   i+=1;
	   }
	   bd.amtOfBlocks = arr.Length;
	   bf.Serialize(file,bd);
	   file.Close();
    }
	
    internal void LoadBlocks()
    {
		if(File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
		{
		   BinaryFormatter bf = new BinaryFormatter();
		   FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
		   BlockData bd = (BlockData)bf.Deserialize(file);
		   file.Close();
		   for(int i=0; i<bd.amtOfBlocks; i++){
			   AbstractBlock abstractBlock = block.GetComponent<AbstractBlock>();
			   abstractBlock.life = bd.life[i];
			   SpriteRenderer rend = block.GetComponent<SpriteRenderer>();
			   rend.color = new Color(bd.color[i,0],bd.color[i,1],bd.color[i,2],bd.color[i,3]);
			   Instantiate(block, new Vector3(bd.position[i,0],bd.position[i,1], 0), Quaternion.identity);//4x6 klocow
		   }
		}
	}
    internal void ForgotBlocks()
    {
        
    }
}

[Serializable]
class BlockData
{
	public int amtOfBlocks;
	public int[] life;//
	public float[,] color;
	public float[,] position;
	public BlockData(int size){
		color = new float[size,4];
		position = new float[size,2];
		life = new int[size];
	}
}