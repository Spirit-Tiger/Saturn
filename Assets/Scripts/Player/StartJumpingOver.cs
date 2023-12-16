using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJumpingOver : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.Player.transform.GetChild(0).GetComponent<Animator>().Play("JumpOver");
    }
}
