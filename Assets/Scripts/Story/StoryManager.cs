using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    public TextMeshProUGUI TaskTextUI;
    public TaskSO[] Tasks;
    public TaskSO CurrentTask;
    public Image BlackScreen;
    public int ReadedNotes = 0;


    public PlayableDirector Director;
    public GameObject FinishScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < Tasks.Length; i++)
        {
            Tasks[i].TaskId = i;
        }
    }

    private void Update()
    {
        if (ReadedNotes == 3)
        {
            SetTask(3);
        }
    }

    public void StartFirstTask()
    {
        CurrentTask = Tasks[0];
        TaskTextUI.transform.parent.gameObject.SetActive(true);
        TaskTextUI.text = CurrentTask.TaskDescription;
    }

    public void FinishTask()
    {
        CurrentTask.Finfished = true;
        TaskTextUI.transform.parent.gameObject.SetActive(false);
        if (Tasks[CurrentTask.TaskId + 1].TaskTriger != null)
        {
            Tasks[CurrentTask.TaskId + 1].TaskTriger.SetActive(true);
        }
    }

    public void NextTask()
    {
        if (Tasks[CurrentTask.TaskId + 1] != null)
        {

            CurrentTask = Tasks[CurrentTask.TaskId + 1];
            TaskTextUI.text = CurrentTask.TaskDescription;
            TaskTextUI.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            TaskTextUI.transform.parent.gameObject.SetActive(false);
        }
    }


    public void SetTask(int id)
    {

            CurrentTask = Tasks[id];
            TaskTextUI.text = CurrentTask.TaskDescription;
            TaskTextUI.transform.parent.gameObject.SetActive(true);

    }


    public IEnumerator PlayTimelineRoutine()
    {
        //Director.Play();

        GameManager.Instance.CanAct = false;
        GameManager.Instance.UnlockCursor();
        Cursor.visible = true;
        FinishScreen.SetActive(true);
        yield return new WaitForSeconds(0);
        GameManager.Instance.CanAct = false;
        GameManager.Instance.UnlockCursor();
        Cursor.visible = true;
        FinishScreen.SetActive(true);

    }
}
