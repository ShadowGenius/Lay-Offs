using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCpathfinding: MonoBehaviour
{
    public float speed = 3f;
    private List<Vector2> currentPath;
    private int pathIndex;
    public List<string> pointNames;
    public List<Vector2> pointLocations;
    private Dictionary<string, Vector2> Locations;
    private Vector2 moveTarget;
    private bool pathFinished;
    public GameObject child;
    private float lastX;
    private float currentX;
    private string currentTask;
    private List<string> taskList;
    private MapManager mapManager;


    void Start()
    {
        Locations = new Dictionary<string, Vector2>();
        for (int i = 0; i < pointLocations.Count; i++)
        {
            Locations.Add(pointNames[i], pointLocations[i]);
        }
        InitializeTasks();
        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = Locations[currentTask];
        mapManager = FindObjectOfType<MapManager>();
        //Debug.Log("Npc move to " + moveTarget);
        move(moveTarget);
        for(int i = 0; i < pointLocations.Count; i++)
        {
            if (pointLocations == null || pointLocations.Count == 0)
            {
                return;
            }
            //Debug.Log("N: " + pointNames[i] + " L: " + pointLocations[i]);
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
        /*Vector2 oldTarget = moveTarget;
        while (oldTarget == moveTarget)
        {
            moveTarget = pointLocations[Random.Range(0, pointLocations.Count)];
        }*/
        if(taskList.Count == 0)
        {
            InitializeTasks();
        }
        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = Locations[currentTask];
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
        List<string> tempTasks = new List<string>();
        taskList = new List<string>();
        tempTasks.AddRange(pointNames);
        for(int i = 0; i < pointNames.Count; i++)
        {
            int removeIndex = Random.Range(0, tempTasks.Count);
            taskList.Add(tempTasks[removeIndex]);
            tempTasks.RemoveAt(removeIndex);
        }
    }
}
