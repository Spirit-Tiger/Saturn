using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Shooting _gun;
    [SerializeField]
    private int _ammoCount = 5;
    public void Interact()
    {
        _gun.LoadGun(_ammoCount);
       Destroy(gameObject);
    }
}
