using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static class Key
    {
        public const string LastScore = "LastScore";
        public const string BestScore = "BestScore";
        public const string LastBall = "BallName";
        public const string BallAmount = "BallAmount";
        public const string GameIsSaved = "GameIsSaved";
    }
    public int InitalBallsAmount = 10;
    public const string DefaultBallName = "BlueBall";
    public Text bestText;
    public Text scoreText;
    public Text ballsText;

    private BlocksManager bm;
    private int score = 0;
    private int bestScore = 0;
    private int ballsAmount;
    public void Start()
    {
        bm = GetComponent<BlocksManager>();
        if (PlayerPrefs.HasKey(Key.GameIsSaved) == true)
        {
            bm.LoadBlocks();
        }
        else
            bm.GenerateNewLineOfBlocks();
        
        bestScore = ReadInteger(Key.BestScore);
        score = ReadInteger(Key.LastScore);
        ballsAmount = ReadInteger(Key.BallAmount,InitalBallsAmount);
        updateBallsAmount();
        updateScore();
        updateBest();
    }

    private void updateScore()
    {
        scoreText.text = "Score: "+score.ToString();
    }
    private void updateBallsAmount()
    {
        ballsText.text = "Balls: "+ballsAmount.ToString();
    }

    private void updateBest()
    {
        bestText.text = "Best: "+ bestScore.ToString();
    }

    public void IncreaseScore(int value)
    {
        score += value;
        updateScore();
    }
    internal void IncreaseBallsAmount()
    {
        ballsAmount += 1;
        updateBallsAmount();
    }

    internal int ReadInteger(string key, int defaultValue = 0)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetInt(key);
        else
            return defaultValue;
    }
       
    internal string ReadBallName()
    {
        if (PlayerPrefs.HasKey("BallName"))
            return PlayerPrefs.GetString("BallName");
        else
            return DefaultBallName;
    }

    public void SaveGame()
    {
        ShootingController controller = GetComponent<ShootingController>();
        PlayerPrefs.SetInt(Key.LastScore, score);
        PlayerPrefs.SetInt(Key.BallAmount,controller.Amount);
        PlayerPrefs.SetString(Key.LastBall, controller.BallName);
        PlayerPrefs.SetInt(Key.GameIsSaved, 1);
        bm.SaveBlocks();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }

    internal void LostGame()
    {
        PlayerPrefs.DeleteKey(Key.LastBall);
        PlayerPrefs.DeleteKey(Key.BallAmount);
        PlayerPrefs.DeleteKey(Key.LastScore);
        bm.ForgotBlocks();
        //set new best score
        if(bestScore < score)
        {
            PlayerPrefs.SetInt(Key.BestScore, score);
            PlayerPrefs.Save();
        }
    }


}
