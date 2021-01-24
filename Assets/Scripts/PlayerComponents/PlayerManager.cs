using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameStatus
{
    LOST,
    ON,
}
public static class Key
{
    public const string LastScore = "LastScore";
    public const string BestScore = "BestScore";
    public const string LastBall = "BallName";
    public const string BallAmount = "BallAmount";
    public const string GameIsSaved = "GameIsSaved";
    public const string HardnessLevel = "HardnessLevel";
}
public class PlayerManager : MonoBehaviour
{
    public int InitalBallsAmount = 1;
    public const string DefaultBallName = "BlueBall";
    public Text bestText;
    public Text scoreText;
    public Text ballsText;
    public Canvas canvasGameOverModel;
    public GameObject GameController;

    private GameStatus gameStatus = GameStatus.ON;
    private BlocksManager bm;
    private int score = 0;
    private int bestScore = 0;
    private int ballsAmount;

    public static void ForgetPlayerVariables()
    {
        int bestScore = PlayerPrefs.GetInt(Key.BestScore);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(Key.BestScore, bestScore);
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

    /// <returns> false if round can not be started, which means the game is lost, otherwise true</returns>
    public bool StartNextRound()
    {   
        if (gameStatus == GameStatus.LOST)
            return false;
        bm.GenerateNewLineOfBlocks();
        if (bm.checkIfLost() == false)
        {
            updateBallsAmount();
            SaveGame();
            return true;
        }
        else // it s a lost game, end..
        {
            LostGame();
            return false;
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
        bm.SaveBlocks();
        PlayerPrefs.SetInt(Key.GameIsSaved, 1);
        PlayerPrefs.Save();
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    internal void LostGame()
    {
        gameStatus = GameStatus.LOST;
        ForgetPlayerVariables();
        bm.ForgetBlocks();
        setNewBestScore();
        Instantiate(canvasGameOverModel);
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
