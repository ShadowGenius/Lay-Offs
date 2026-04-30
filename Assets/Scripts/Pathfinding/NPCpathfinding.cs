using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCpathfinding: MonoBehaviour
{
    [SerializeField] NPC personality;
    public float speed = 3f;
    private List<Vector2> currentPath;
    private int pathIndex;
    public List<OfficeTask> officeTasks;
    private Dictionary<string, Vector2> Locations;
    private Vector2 moveTarget;
    private bool pathFinished;
    public GameObject child;
    private float lastX;
    private float currentX;
    private OfficeTask currentTask;
    private MapManager mapManager;
    [SerializeField] MapManager pathfindingMap;
    [SerializeField] private List<OfficeTask> taskList;


    void Start()
    {
        personality = gameObject.GetComponent<NPC>();
        officeTasks = pathfindingMap.TaskList;
        InitializeTasks();
        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = currentTask.taskLocation;
        mapManager = FindObjectOfType<MapManager>();
        //Debug.Log("Npc move to " + moveTarget);
        move(moveTarget);
        for(int i = 0; i < officeTasks.Count; i++)
        {
            if (officeTasks == null || officeTasks.Count == 0)
            {
                return;
            }
        }
    }

    public void move(Vector2 targetPos)
    {
        currentPath = mapManager.FindPath((Vector2)transform.position, targetPos);
        //Debug.Log("Current path " + currentPath);
        pathIndex = 0;
    }

    private void Update()
    {
        if(pathFinished)
        {
            return;
        }
        if (currentPath == null)
        {
            Debug.Log("null," + " Trying to get to " + moveTarget);
            return;
        }
        if (pathIndex >= currentPath.Count)
        {
            //Debug.Log("finished");
            pathFinished = true;
            StartCoroutine(WaitABit());
            return;
        }

        //Debug.Log("continue");
        Vector2 target = currentPath[pathIndex];
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        currentX = transform.position.x;
        if(currentX >= lastX)
        {
            //Debug.Log("Right " + currentX + " bigger than  " + lastX);
            child.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            //Debug.Log("Left " + currentX + " less than  " + lastX);
            child.GetComponent<SpriteRenderer>().flipX = false;
        }
        lastX = currentX;
        if((Vector2)transform.position == target)
        {
            pathIndex++;
        }
    }

    IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(3f);

        bool taskIncomplete = false;
        if (currentTask.taskActive == false)
        {
            Debug.Log(gameObject.name + " can't do their task!");
            taskIncomplete = true;
        }
        if(taskList.Count == 0)
        {
            InitializeTasks();
        }
        if (taskIncomplete)
        {
            taskList.Remove(currentTask);
            taskList.Add(currentTask);
        }
        else if (Random.Range(0.0f, 0.1f) < personality.sabatogeChance)
        {
            int taskIndex = -1;
            int i = 0;
            while (taskIndex == -1)
            {
                Debug.Log("TASK" + taskIndex);
                if (currentTask.taskName == taskList[i].taskName)
                {
                    taskIndex = i;
                }
                i++;
            }
            pathfindingMap.TaskList[taskIndex].taskActive = false;
            Debug.Log("SABATOGE");
        }

        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = currentTask.taskLocation;
        Debug.Log("Employee going to " + currentTask);
        currentPath = mapManager.FindPath((Vector2)transform.position, moveTarget);
        pathIndex = 0;
        pathFinished = false;
    }

    public void GoTo(Vector2 location)
    {
        currentPath = mapManager.FindPath((Vector2)transform.position, location);
        pathIndex = 0;
        pathFinished = false;
    }
    public void GoTo(float locationx, float locationy)
    {
        Vector2 location = new Vector2(locationx, locationy);
        currentPath = mapManager.FindPath((Vector2)transform.position, location);
        pathIndex = 0;
        pathFinished = false;
    }

    public void GoTo(string locationStr)
    {
        moveTarget = Locations[locationStr];
        currentPath = mapManager.FindPath((Vector2)transform.position, moveTarget);
        pathIndex = 0;
        pathFinished = false;
    }

    public void InitializeTasks()
    {
        Debug.Log("Initializetasks");
        List<OfficeTask> tempTasks = new List<OfficeTask>();
        taskList = new List<OfficeTask>();
        for(int i = 0; i < officeTasks.Count; i++)
        {
            tempTasks.Add(officeTasks[i]);
        }
        for(int i = 0; i < officeTasks.Count; i++)
        {
            int removeIndex = Random.Range(0, tempTasks.Count);
            taskList.Add(tempTasks[removeIndex]);
            tempTasks.RemoveAt(removeIndex);
        }
        /*taskList.Add(pointNames[0]);
        taskList.Add(pointNames[1]);
        taskList.Add(pointNames[2]);*/
    }
}
