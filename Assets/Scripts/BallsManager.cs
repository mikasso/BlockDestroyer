using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
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

}
