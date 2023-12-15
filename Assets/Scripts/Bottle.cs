using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
        GameManager.Instance.HasBottle = true;
    }
}
