using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    [SerializeField]
    private float _dealy = 0.1f;
    private void OnEnable()
    {
        StartCoroutine(DisableLight());
    }

    private IEnumerator DisableLight()
    {
        yield return new WaitForSeconds(_dealy);
        gameObject.SetActive(false);
    }
}
