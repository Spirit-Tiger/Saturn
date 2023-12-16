using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalScreenTrigger : MonoBehaviour
{
    public PlayableDirector Director;
    public GameObject FinishScreen;

    public IEnumerator PlayTimelineRoutine(PlayableDirector playableDirector)
    {
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.duration);
        GameManager.Instance.CanAct = false;
        GameManager.Instance.UnlockCursor();
        Cursor.visible = true;
        FinishScreen.SetActive(true);
    }
}
