using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    private Light _flashlight;
    private AudioSource _flashlightSound;
    private void Start()
    {
        _flashlight = GetComponentInChildren<Light>();
        _flashlightSound = GetComponentInChildren<AudioSource>();
        _flashlight.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.InNote == false && GameManager.Instance.CanAct)
        {
            _flashlightSound.Play();
            _flashlight.enabled = !_flashlight.enabled;
        }
    }
}
