using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCpathfinding: MonoBehaviour
{
    //A bunch of variables are initialized here. I tried to make the names clear enough but if you're confused about any of them feel free to ask.
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
    private bool active;
    private OfficeTask currentTask;
    private MapManager mapManager;
    [SerializeField] MapManager pathfindingMap;
    [SerializeField] private List<OfficeTask> taskList;


    void Start()
    {
        //Adding a bunch of things to the variables when the game starts. Stuff I didn't want to have to do in the inspector window which would've cluttered it.
        personality = gameObject.GetComponent<NPC>();
        officeTasks = pathfindingMap.TaskList;
        InitializeTasks();
        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = currentTask.taskLocation;
        mapManager = FindObjectOfType<MapManager>();
        //Debug.Log("Npc move to " + moveTarget);
        move(moveTarget);
        //I forget what this did
        for(int i = 0; i < officeTasks.Count; i++)
        {
            if (officeTasks == null || officeTasks.Count == 0)
            {
                return;
            }
        }
    }
    //This function grabs a list of the fastest position from where they are to wherever they're trying to get to and stores it in currentPath.
    //It also resets the pathIndex so the script knows it's at the beginning of a new path.
    public void move(Vector2 targetPos)
    {
        currentPath = mapManager.FindPath((Vector2)transform.position, targetPos);
        //Debug.Log("Current path " + currentPath);
        pathIndex = 0;
    }

    private void Update()
    {
        //Only run the movement code if the NPC is currently active and not at a meeting or something.
        if (active)
        {
            //If the path has been finished then just return the function immediately. The variable will be set back to false elsewhere.
            if (pathFinished)
            {
                return;
            }
            //Used for testing, if the currentPath variable doesn't have anything in it then something went wrong when getting the path.
            if (currentPath == null)
            {
                Debug.Log("null," + " Trying to get to " + moveTarget);
                return;
            }
            //The NPC has reached their destination and has run out of elements in the pathIndex list.
            if (pathIndex >= currentPath.Count)
            {
                //Debug.Log("finished");
                pathFinished = true;
                //Starts the Coroutine for the actual task stuff.
                StartCoroutine(CompleteTaskEnumerator());
                return;
            }

            //The rest of the code here is used to move the npc along its path.
            Vector2 target = currentPath[pathIndex];
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            currentX = transform.position.x;
            if (currentX >= lastX)
            {
                child.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                child.GetComponent<SpriteRenderer>().flipX = false;
            }
            lastX = currentX;
            if ((Vector2)transform.position == target)
            {
                pathIndex++;
            }
        }
    }

    //The actual task logic.
    IEnumerator CompleteTaskEnumerator()
    {
        //Waits for 3 seconds.
        yield return new WaitForSeconds(3f);

        //Checks to see if the task is currently active. Checks the task from the original TasksScript script since that has the most recent information about the task availability.
        bool taskIncomplete = false;
        if (pathfindingMap.TaskList[currentTask.taskIndex].taskActive == false)
        {
            Debug.Log(gameObject.name + " can't do their task! " + currentTask.taskName);
            taskIncomplete = true;
        }
        //If the employee has finished their tasks, just give them new tasks for the moment. May be changed later.
        if(taskList.Count == 0)
        {
            InitializeTasks();
        }
        //The task was not completed by the employee so they'll save it for later. Makes sure it gets added at the very end.
        if (taskIncomplete)
        {
            if (taskList.Contains(currentTask))
            {
                taskList.Remove(currentTask);
            }
            taskList.Add(currentTask);
        }
        else
        {
            //Run the OnNPCUse function from the script of the object we're interacting with.
            currentTask.taskObject.GetComponent<ObjectInteraction>().OnNPCUse(personality);

            //Run a chance for the npc to sabatoge the task making it inactive for everyone else.
            if (Random.Range(0.0f, 0.1f) < personality.sabatogeChance)
            {
                int taskIndex = currentTask.taskIndex;
                pathfindingMap.TaskList[taskIndex].taskActive = false;
                Debug.Log(gameObject.name + " SABOTAGED " + currentTask.taskName + " and it is now " + pathfindingMap.TaskList[taskIndex].taskActive);
                currentTask.taskObject.GetComponent<ObjectInteraction>().OnNPCSabotage(personality);
            }
        }
        //Start the process of setting a new task as the current task and resetting their movement variables such as pathIndex and pathFinished so the npc knows to start moving again
        currentTask = taskList[0];
        taskList.RemoveAt(0);
        moveTarget = currentTask.taskLocation;
        //Debug.Log("Employee going to " + currentTask);
        currentPath = mapManager.FindPath((Vector2)transform.position, moveTarget);
        pathIndex = 0;
        pathFinished = false;
    }
    
    //Several GoTo functions that do the same thing but with different parameters to make it easier on coders.
    //It looks like they do the exact same thing as move? I'm not really sure why I did that.
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
        //Debug.Log("Initializetasks");
        //Grab a temporary list of all the tasks.
        List<OfficeTask> tempTasks = new List<OfficeTask>();
        taskList = new List<OfficeTask>();
        for(int i = 0; i < officeTasks.Count; i++)
        {
            tempTasks.Add(officeTasks[i]);
        }
        //Add the tasks to the list of tasks the employee personally has. Currently it just adds all of them but we can change that if we want to.
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
    
    //Teleports the NPC. For use by other scripts. Two functions so you can either pass a vector3 or 3 floats.
    public void teleport(Vector3 pos)
    {
        transform.position = pos;
    }

    public void teleport(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
