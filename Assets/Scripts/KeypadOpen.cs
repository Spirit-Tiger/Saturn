using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadOpen : MonoBehaviour, IInteractable
{
    public GameObject Keypad;
    public void Interact()
    {
        Keypad.SetActive(true);
    }
}
 