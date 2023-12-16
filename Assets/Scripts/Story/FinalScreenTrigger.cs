using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScreenTrigger : MonoBehaviour
{
    public GameObject FinishScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.CanAct = false;
            GameManager.Instance.UnlockCursor();
            Cursor.visible = true;
            FinishScreen.SetActive(true);
        }
    }
}
