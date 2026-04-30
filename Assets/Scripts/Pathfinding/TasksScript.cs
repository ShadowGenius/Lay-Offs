using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OfficeTask
{
    public string taskName;
    public Vector2 taskLocation;
    public GameObject taskObject;
    public bool taskActive = true;
}


[CreateAssetMenu(fileName = "TaskList", menuName ="Custom/Task List")]
public class TasksScript : ScriptableObject
{
    public List<OfficeTask> TasksList = new List<OfficeTask>();
}
