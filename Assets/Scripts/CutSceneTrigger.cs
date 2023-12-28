using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    public PlayableDirector CutScene;
    public CharacterController Controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Controller.enabled = false;
            CutScene.enabled = true;
            GameManager.Instance.CanAct = false;
            Invoke("EnableActing", (float)CutScene.duration);
        }
    }

    void EnableActing()
    {
        GameManager.Instance.CanAct = true;
        Controller.enabled = true;
        Destroy(gameObject);
    }


    
}
