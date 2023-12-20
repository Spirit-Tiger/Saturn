using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHand : MonoBehaviour
{
    [SerializeField]
    private GameObject _gun;

    [SerializeField]
    private GameObject _falshlight;

    [SerializeField]
    private GameObject _bottle;

    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gun.SetActive(false);
            _bottle.SetActive(false);
            _falshlight.SetActive(true);
            animator.SetTrigger("FlashLight");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.Instance.HasGun)
        {
            _gun.SetActive(true);
            _bottle.SetActive(false);
            _falshlight.SetActive(false);
            animator.SetTrigger("Pistol");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.Instance.HasBottle)
        {
            _bottle.SetActive(true);
            _gun.SetActive(false);
            _falshlight.SetActive(false);
            //animator.SetTrigger("Bottle");
        }
    }
}
