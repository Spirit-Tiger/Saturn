using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NoteScript : MonoBehaviour,IInteractable
{
    [SerializeField]
    private string _noteText;

    public  GameObject _noteUI;
    private bool _noteIsOpened = false;
    private bool _readed = false;

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
        if (!_readed)
        {
            StoryManager.Instance.ReadedNotes++;
            _readed = true;
        }
        _noteIsOpened = true;
        _noteUI.SetActive(true); 
        _noteUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _noteText;
        GameManager.Instance.InNote = true;
        GameManager.Instance.CanAct = false;
       
    }
}
