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

    private MapManager mapManager;


    void Start()
    {
        moveTarget = pointLocations[Random.Range(0, pointLocations.Count)];
        mapManager = FindObjectOfType<MapManager>();
        move(moveTarget);
    }

    public void move(Vector2 targetPos)
    {
        currentPath = mapManager.FindPath((Vector2)transform.position, targetPos);
        pathIndex = 0;
    }

    private void Update()
    {
        if (pathIndex >= currentPath.Count)
        {
            Debug.Log("Path Finished");
            pathFinished = true;
            return;
        }
        if (currentPath == null)
            return;
        Vector2 target = currentPath[pathIndex];
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if((Vector2)transform.position == target)
        {
            pathIndex++;
        }
    }

    IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("EndSeconds");
        currentPath.Clear();
        pathFinished = false;
        moveTarget = pointLocations[Random.Range(0, pointLocations.Count)];
    }

    public void GoTo(Vector2 location)
    {
        currentPath.Clear();
        pathFinished = false;
        moveTarget = location;
    }

    public void GoTo(string location)
    {
        Debug.Log("New Goto");
        moveTarget = new Vector2(4, 3);
        currentPath = mapManager.FindPath((Vector2)transform.position, moveTarget);
        pathIndex = 0;
        pathFinished = false;
    }
}
