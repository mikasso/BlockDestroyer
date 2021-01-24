using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public bool SpeedUp = false;
    public void killAllBalls()
    {
        GameObject[] balls = getBallsGameObjectsArr();
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
    }

    private GameObject[] getBallsGameObjectsArr()
    {
        return GameObject.FindGameObjectsWithTag("Ball");
    }

    public void speedUpUntilThereAreAnyBalls()
    {
        SpeedUp = true;
        Time.timeScale = 3;
    }

    private void Update()
    {
        if (SpeedUp == true)
        {
            if(GameObject.FindGameObjectWithTag("Ball") == null) // none of balls is alive
            {
                Time.timeScale = 1;
                SpeedUp = false;
            }
        }
    }

    public void OnDisable()
    {
        Time.timeScale = 1;
        SpeedUp = false;
    }


}
