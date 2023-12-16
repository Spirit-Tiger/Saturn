using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTask",menuName = "Tasks")]
public class TaskSO : ScriptableObject
{
    public int TaskId;
    public string TaskName;
    public string TaskDescription;
    public bool Finfished;
    public GameObject TaskTriger;
}
