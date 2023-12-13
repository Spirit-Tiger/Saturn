using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject PauseMenu;

    public bool CanMoveCamera = true;
    public bool CanShoot = true;
    public bool CanAct = true;
    public bool IsPaused = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //CanAct = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Debug.Log("Update");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused == true)
            {
                CanAct = true;
                IsPaused = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                HideCursor();


            }
            else if (IsPaused == false)
            {
                Debug.Log("Pause");
                UnlockCursor();
                CanAct = false;
                IsPaused = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
