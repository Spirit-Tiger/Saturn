using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NoteScript : MonoBehaviour,IInteractable
{
    [SerializeField]
    private string _noteText;

    private GameObject _uiCanvas;
    private GameObject _noteUI;
    private bool _noteIsOpened = false;


    private void Awake()
    {
        _uiCanvas = GameObject.Find("Canvas");
        _noteUI = _uiCanvas.transform.Find("NoteUI").gameObject;
    }

    private void Update()
    {
        if (_noteIsOpened && Input.GetKeyDown(KeyCode.Escape))
        {
            _noteIsOpened = false;
            _noteUI.SetActive(false);
            GameManager.Instance.InNote = false;
            GameManager.Instance.CanAct = true;
        }
    }

    public void Interact()
    {
        
        _noteIsOpened = true;
        _noteUI.SetActive(true); 
        _noteUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _noteText;
        GameManager.Instance.InNote = true;
        GameManager.Instance.CanAct = false;
       
    }
}
