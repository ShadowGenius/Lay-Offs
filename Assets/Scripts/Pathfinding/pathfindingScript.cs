using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pathfindingScript : MonoBehaviour
{

    [SerializeField] private int gridHeight = 10;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private float cellHeight = 1f;
    [SerializeField] private float cellWidth = 1f;

    [SerializeField] private bool generatePath;
    [SerializeField] private bool visualizeGrid;

    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (generatePath && !pathGenerated)
        {
            GenerateGrid();
            pathGenerated = true;
        }
        else if (!generatePath)
        {
            pathGenerated = false;
        }
    }

    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();

        for(float x = 0; x < gridWidth; x += cellWidth)
        {
            for(float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }
        for(int i = 0; i < 40; i++)
        {
            Vector2 pos = new Vector2(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
            cells[pos].isWall = true;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.Log("Testing");
        if (!visualizeGrid || cells == null)
        {
            return;
        }
        foreach (KeyValuePair<Vector2, Cell> kvp in cells)
        {
            if(!kvp.Value.isWall)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }
            Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth, cellHeight));
        }
    }

    private class Cell
    {
        public Vector2 position;
        public int fCost;
        public int gCost;
        public int hCost;
        public Vector2 connection;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}
