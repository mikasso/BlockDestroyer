using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnNewGameButton()
    {
        PlayerManager.ForgetPlayerVariables();
        SceneManager.LoadScene("GameScene");
    }

    public void OnLoadGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
