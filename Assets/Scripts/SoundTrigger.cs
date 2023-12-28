using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour 

{
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
        {
		GetComponent<AudioSource>().Play();
		}
	}
}