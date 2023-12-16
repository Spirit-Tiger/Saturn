using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StoryManager.Instance.SetTask(2);
            gameObject.SetActive(false);
        }
    }
}
 