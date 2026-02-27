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
        Locations = new Dictionary<string, Vector2>();
        moveTarget = pointLocations[Random.Range(0, pointLocations.Count)];
        mapManager = FindObjectOfType<MapManager>();
        Debug.Log("Npc move to " + moveTarget);
        move(moveTarget);
        for(int i = 0; i < pointLocations.Count; i++)
        {
            if (pointLocations == null || pointLocations.Count == 0)
            {
                return;
            }
            Debug.Log("N: " + pointNames[i] + " L: " + pointLocations[i]);
            Locations.Add(pointNames[i], pointLocations[i]);
        }
    }

    public void move(Vector2 targetPos)
    {
        currentPath = mapManager.FindPath((Vector2)transform.position, targetPos);
        Debug.Log("Current path " + currentPath);
        pathIndex = 0;
    }

    private void Update()
    {
        if (currentPath == null)
        {
            Debug.Log("null");
            return;
        }
        if (pathIndex >= currentPath.Count)
        {
            Debug.Log("finished");
            pathFinished = true;
            return;
        }

        Debug.Log("continue");
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
        moveTarget = pointLocations[Random.Range(0, pointLocations.Count)];
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
}
