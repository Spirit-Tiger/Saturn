using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class KeypadScript : MonoBehaviour
{
    public TextMeshProUGUI EnteredText;
    public GameObject Crosshair;

    private void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.CanAct = false;
        GameManager.Instance.InNote = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.CanAct = true;
        GameManager.Instance.InNote = false;
        Crosshair.GetComponent<RectTransform>().position = new Vector2(960,540);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        DeleteText();
    }

    private void Update()
    {
        Crosshair.GetComponent<RectTransform>().position = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    public void GetButtonValue(int value)
    {
        EnteredText.text += value;
    }

    public void DeleteText()
    {
        EnteredText.text = string.Empty;
    }

    public void Enter()
    {
        Debug.Log("EnteredText");
        if (Int32.Parse(EnteredText.text) == 0832)
        {

          StartCoroutine(StoryManager.Instance.PlayTimelineRoutine());
            gameObject.SetActive(false);
        }
        else
        {
            DeleteText();
        }
    }
}
