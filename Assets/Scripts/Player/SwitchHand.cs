using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHand : MonoBehaviour
{
    [SerializeField]
    private GameObject _gun;

    [SerializeField]
    private GameObject _falshlight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gun.SetActive(false);
            _falshlight.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _gun.SetActive(true);
            _falshlight.SetActive(false);
        }
    }
}
