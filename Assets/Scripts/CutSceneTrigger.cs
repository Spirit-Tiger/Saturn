using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    public PlayableDirector CutScene;
    public Collider BoxCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BoxCollider.enabled = false;
            CutScene.enabled = true;
            GameManager.Instance.CanAct = false;
            Invoke("EnableActing", (float)CutScene.duration);
        }
    }

    void EnableActing()
    {
        GameManager.Instance.CanAct = true;
        Destroy(gameObject);
    }


    
}
