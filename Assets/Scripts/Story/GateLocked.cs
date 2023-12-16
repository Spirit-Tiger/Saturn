using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLocked : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StoryManager.Instance.FinishTask();
        GetComponent<AudioSource>().Play();
        StartCoroutine(GateOff());
    }

    private IEnumerator GateOff()
    {
        yield return new WaitForSeconds(2f);
        StoryManager.Instance.NextTask();
        gameObject.SetActive(false);
    }


}
