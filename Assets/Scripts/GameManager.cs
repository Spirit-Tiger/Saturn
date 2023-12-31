using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject PauseMenu;
    public GameObject DialogScreen;
    public GameObject Crosshair;
    public GameObject Player;
    public GameObject CameraPoint;
    public GameObject CurrentLadder;

    public Transform RespawnPoint;
    public Transform Bottle;
    public Vector3 BottlePosition;

    public Vector3 LadderStartPosition;

    public AudioSource Music;

    public bool CanMoveCamera = true;
    public bool CanShoot = true;
    public bool CanAct = false;
    public bool IsPaused = false;
    public bool InGame = false;
    public bool InDialogue = false;
    public bool InNote = false;
    public bool IsOnLadder = false;
    public bool IsExitingLadder = false;
    public bool IsEnteringLadderDown = false;
    public bool HasBottle = false;
    public bool HasGun = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        BottlePosition = Bottle.localPosition;
    }

    private void Start()
    {
        /*        CanAct = false;
                UnlockCursor();
                HideCrosshair();*/
        CanAct = true;
        HideCursor();
        ShowCrosshair();
    }

    private void Update()
    {
        if(IsPaused == false)
        {
            Music.UnPause();
        }
        if (Input.GetKeyDown(KeyCode.P) && InGame)
        {
            if (IsPaused == true)
            {
                if (InDialogue == false)
                {
                    Debug.Log("UnPause game");
                    CanAct = true;
                    HideCursor();
                }
                IsPaused = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
            else if (IsPaused == false)
            {
                Music.Pause();
                UnlockCursor();
                Cursor.visible = true;
                CanAct = false;
                IsPaused = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (InDialogue)
        {
            if (IsPaused)
            {
                DialogScreen.GetComponent<AudioSource>().Pause();
            }
            else
            {
                DialogScreen.GetComponent<AudioSource>().UnPause();
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

    public void StartGame()
    {
        InGame = true;
        InDialogue = true;
        DialogScreen.SetActive(true);
    }

    public void HideCrosshair()
    {
        Crosshair.SetActive(false);
    }

    public void ShowCrosshair()
    {
        Crosshair.SetActive(true);
    }
}

