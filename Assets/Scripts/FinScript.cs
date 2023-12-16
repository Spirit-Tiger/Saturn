using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinScript : MonoBehaviour
{
    public GameObject FinScreen;
    public PlayableDirector FinDirector;

    private void Awake()
    {
        StartCoroutine(FinGame());   
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator FinGame()
    {
        yield return new WaitForSeconds((float)FinDirector.duration);
        Debug.Log("Cool");
        FinScreen.SetActive(true);
    }
}
