using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeGun : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.HasGun= true;
        gameObject.SetActive(false);
    }
}
