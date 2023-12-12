using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private DialogComponent[] _dialogObjects;
    [SerializeField]
    private Image _backgroundPanel;
    [SerializeField]
    private float _letterDelay = 0.02f;

    private AudioSource _typingAudio;

    private int _objectCounter = 0;


    [SerializeField]
    private GameObject _showNextMessageButton;
    [SerializeField]
    private GameObject _finishDialogButton;
    private bool _canShowNextMessage = false;
    private bool _canFinish = false;

    private bool _fadeOut = false;

    private void Awake()
    {
        _typingAudio = GetComponent<AudioSource>();
        _backgroundPanel = GetComponent<Image>();
    }

    private void Start()
    {
        NextMessage();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) )
        {
            if (_canShowNextMessage)
            {
                NextMessage();
            }
            if (_canFinish)
            {
                FinishDialogue();
            }
        }

        if (_fadeOut)
        {
            if(_backgroundPanel.color.a >= 0)
            {
                var tempColor = _backgroundPanel.color;
                tempColor.a = Mathf.Lerp(tempColor.a,0,0.2f);
                _backgroundPanel.color = tempColor;
                Debug.Log(_backgroundPanel.color.a);
                if (_backgroundPanel.color.a <= 0.001f)
                {
                    GameManager.Instance.HideCursor();
                    GameManager.Instance.CanAct = true;
                    _fadeOut = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void NextMessage()
    {
        StartCoroutine(TypeLetters());
    }

    public void FinishDialogue()
    {
        _finishDialogButton.SetActive(false);

        for (int i = 0; i < _dialogObjects.Length; i++)
        {
            _dialogObjects[i].gameObject.SetActive(false);
        }

        _fadeOut = true;
    }

    private IEnumerator TypeLetters()
    {
        _canShowNextMessage = false;
        _showNextMessageButton.SetActive(false);


        _dialogObjects[_objectCounter].gameObject.SetActive(true);
        TextMeshProUGUI textObject = _dialogObjects[_objectCounter].TextObject;
        string textToWrite = _dialogObjects[_objectCounter].TextString;

        _typingAudio.Play();
        for (int i = 0; i < textToWrite.Length; i++)
        {
            textObject.text += textToWrite[i];
            yield return new WaitForSeconds(_letterDelay);
        }

        if (_dialogObjects.Length - 1 == _objectCounter)
        {
            _canFinish = true;
            _finishDialogButton.SetActive(true);
           _objectCounter = 0;
        }
        else
        {
            _canShowNextMessage = true;
            _objectCounter++;
            _showNextMessageButton.SetActive(true);
        }
        _typingAudio.Stop();
    }
}
