using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
        Debug.Log("StartGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
