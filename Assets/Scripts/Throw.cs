using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private bool _InAir = false;
    public AudioClip BottleCrash;
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
            gameObject.GetComponent<AudioSource>().PlayOneShot(BottleCrash);
            GameManager.Instance.HasBottle= false;
            gameObject.transform.localPosition = GameManager.Instance.BottlePosition;
            StartCoroutine(DisableBottle());
        }
    }

    private IEnumerator DisableBottle()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
