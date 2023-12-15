using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private bool _InAir = false;
    private void OnEnable()
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
    private void Update()
    {
        if (GameManager.Instance.CanAct && Input.GetMouseButtonDown(0) && GameManager.Instance.HasBottle)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10f,ForceMode.Impulse);
            _InAir = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_InAir)
        {
            gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.HasBottle= false;
            gameObject.transform.localPosition = GameManager.Instance.BottlePosition;
           gameObject.SetActive(false);

        }
    }
}
