using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.IsPaused = false;
        if (GameManager.Instance.InDialogue == false)
        {
            GameManager.Instance.CanAct = true;
            GameManager.Instance.HideCursor();
        }
        GameManager.Instance.PauseMenu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
