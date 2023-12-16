using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTriger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        StoryManager.Instance.StartFirstTask();
        gameObject.SetActive(false); 
    }
}
