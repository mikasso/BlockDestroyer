using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BlocksManager : MonoBehaviour
{
    public GameObject block;
    public const int blocksInRow = 6;
    public const int rows = 4;
    public  float leftX = 0.45f;
    public float topY = 10.0f;
    public float blockSize = 1.0f;
    public float minBlockLevel = 2.0f;

    private string BlocksFilePath;
    public int hardnessLevel = 10;
    private System.Random random = new System.Random();
    void Awake()
    {
        BlocksFilePath = Application.persistentDataPath + "/gameInfo.dat";
    }

    internal void GenerateNewLineOfBlocks()
    {
        if (hardnessLevel < 50)
            hardnessLevel++;
        else
            hardnessLevel += 3;
        generateLine(-1);
        moveAllBlocksLevelsDown(level: 1);
    }
    private void generateLine(int level)
    {
        for (int j = 0; j < blocksInRow; j++)
        {
            Vector3 pos = new Vector3(leftX + blockSize * j, topY - blockSize * level, 0);
            GameObject b = Instantiate(block,pos , Quaternion.identity);//4x6 klocow
            ColorfulBlock colorfulBlock = b.GetComponent<ColorfulBlock>();
            colorfulBlock.ChangeColorToRandom();
            colorfulBlock.Life = random.Next(hardnessLevel / 2, hardnessLevel);
        }
    }

    public bool checkIfLost() {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject blockObject in arr)
            if (blockObject.transform.position.y < minBlockLevel)
                return true;
        return false;
    }

    private void moveAllBlocksLevelsDown(int level){
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject blockObject in arr)
            blockObject.transform.Translate(new Vector3(0, -1.0f));
    }

    internal void SaveBlocks()
    {
       BinaryFormatter bf = new BinaryFormatter();
	   FileStream file = File.Create(BlocksFilePath);
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
		   bd.life[i] = blockObject.GetComponent<AbstractBlock>().Life;
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
		if(File.Exists(BlocksFilePath))
		{
            Debug.Log("File opened");
            BinaryFormatter bf = new BinaryFormatter();
		   FileStream file = File.Open(BlocksFilePath, FileMode.Open);
		   BlockData bd = (BlockData)bf.Deserialize(file);
		   file.Close();
		   for(int i=0; i<bd.amtOfBlocks; i++){
			   AbstractBlock abstractBlock = block.GetComponent<AbstractBlock>();
			   SpriteRenderer rend = block.GetComponent<SpriteRenderer>();
			   rend.color = new Color(bd.color[i,0],bd.color[i,1],bd.color[i,2],bd.color[i,3]);
			   GameObject newBlock = Instantiate(block, new Vector3(bd.position[i,0],bd.position[i,1], 0), Quaternion.identity);
               newBlock.GetComponent<AbstractBlock>().Life = bd.life[i];
		   }
        }
        else
        {
            Debug.Log(BlocksFilePath +  "File doesnt exist");
        }
	}
    internal void ForgetBlocks()
    {
        File.Delete(BlocksFilePath);
    }

    [Serializable]
    class BlockData
    {
        public int amtOfBlocks;
        public int[] life;//
        public float[,] color;
        public float[,] position;
        public BlockData(int size)
        {
            color = new float[size, 4];
            position = new float[size, 2];
            life = new int[size];
        }
    }
}
