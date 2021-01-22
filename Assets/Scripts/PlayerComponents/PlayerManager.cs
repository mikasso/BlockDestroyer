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
    public int InitalBallsAmount = 1;
    public const string DefaultBallName = "BlueBall";
    public Text bestText;
    public Text scoreText;
    public Text ballsText;
    public Canvas canvasGameOverModel;
    public GameObject GameController;

    private BlocksManager bm;
    private Canvas displayedCanvas;
    private int score = 0;
    private int bestScore = 0;
    private int ballsAmount;

    public static void ForgetPlayerVariables()
    {
        PlayerPrefs.DeleteKey(Key.LastBall);
        PlayerPrefs.DeleteKey(Key.BallAmount);
        PlayerPrefs.DeleteKey(Key.LastScore);
        PlayerPrefs.DeleteKey(Key.GameIsSaved);
    }
    public void Start()
    {
        bm = GameController.GetComponent<BlocksManager>();
        canvasGameOverModel.worldCamera = Camera.main;
        if (PlayerPrefs.HasKey(Key.GameIsSaved) == true)
        {
            Debug.Log("try load");
            bm.LoadBlocks();
        }
        else
        {
            Debug.Log("nothing to load");
            bm.GenerateNewLineOfBlocks();
        }
        bestScore = ReadInteger(Key.BestScore);
        score = ReadInteger(Key.LastScore);
        ballsAmount = ReadInteger(Key.BallAmount, InitalBallsAmount);
        updateBallsAmount();
        updateScore();
        updateBest();
    }

    public void AfterShootingJobs()
    {
        bm.GenerateNewLineOfBlocks();
        if (bm.checkIfLost() == false)
        {
            updateBallsAmount();
            SaveGame();
            Debug.Log("saved");
        }
        else // it s a lost game, end..
        {
            LostGame();
            enabled = false; // turn off this component.. no more shooting ;(
        }
    }
    public void IncreaseScore(int value)
    {
        score += value;
        updateScore();
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
        PlayerPrefs.SetInt(Key.BallAmount, controller.Amount);
        PlayerPrefs.SetString(Key.LastBall, controller.BallName);
        PlayerPrefs.SetInt(Key.GameIsSaved, 1);
        bm.SaveBlocks();
        PlayerPrefs.Save();
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    internal void LostGame()
    {
        ForgetPlayerVariables();
        bm.ForgetBlocks();
        setNewBestScore();
        displayedCanvas = Instantiate(canvasGameOverModel);
    }

    private void setNewBestScore()
    {
        if (bestScore < score)  {
            PlayerPrefs.SetInt(Key.BestScore, score);
            PlayerPrefs.Save();
        }
    }

    private void updateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    private void updateBallsAmount()
    {
        ballsAmount = GetComponent<ShootingController>().Amount;
        ballsText.text = "Balls: " + ballsAmount.ToString();
    }

    private void updateBest()
    {
        bestText.text = "Best: " + bestScore.ToString();
    }
}
