using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OfficeTask
{
    public string taskName;
    public Vector2 taskLocation;
    public GameObject taskObject;
    public bool taskActive = true;
    public int taskIndex;
}


[CreateAssetMenu(fileName = "TaskList", menuName ="Custom/Task List")]
public class TasksScript : ScriptableObject
{
    public List<OfficeTask> TasksList = new List<OfficeTask>();
}
