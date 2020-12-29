using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int InitalBallsAmount = 5;
    public string DefaultBallName = "BlueBall";
    // Start is called before the first frame update
    public int score = 0;
    public void Start()
    {
        score = ReadScore();
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    private int ReadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
            return PlayerPrefs.GetInt("Score");
        else
            return 0;
    }
    internal int ReadAmount()
    {
        if (PlayerPrefs.HasKey("BallsAmount"))
            return PlayerPrefs.GetInt("BallsAmount");
        else
            return InitalBallsAmount;
                
    }
    internal string ReadBallName()
    {
        if (PlayerPrefs.HasKey("BallName"))
            return PlayerPrefs.GetString("BallName");
        else
            return DefaultBallName;
    }

    internal void SaveGame()
    {
        ShootingController controller = GetComponent<ShootingController>();
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("BallsAmount",controller.Amount);
        PlayerPrefs.SetString("BallName", controller.BallName);
        PlayerPrefs.Save();
    }

    internal void LostGame()
    {
        PlayerPrefs.DeleteKey("BallName");
        PlayerPrefs.DeleteKey("BallsAmount");
        PlayerPrefs.DeleteKey("Score");
    }


}
