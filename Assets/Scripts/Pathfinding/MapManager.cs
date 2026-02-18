using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapManager : MonoBehaviour
{

    [SerializeField] private int gridHeight = 10;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private float cellHeight = 1f;
    [SerializeField] private float cellWidth = 1f;

    [SerializeField] private bool generatePath;
    [SerializeField] private bool visualizeGrid;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    public List<Vector2> cellsToSearch;
    public List<Vector2> searchedCells;
    public List<Vector2> finalPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        Debug.Log("Generate Grid");
        cells = new Dictionary<Vector2, Cell>();

        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }
        cells[new Vector2(2, 0)].isWall = true;
        cells[new Vector2(2, 1)].isWall = true;
        cells[new Vector2(2, 2)].isWall = true;
        cells[new Vector2(2, 3)].isWall = true;
        cells[new Vector2(2, 4)].isWall = true;
        cells[new Vector2(2, 5)].isWall = true;
        cells[new Vector2(2, 6)].isWall = true;
        cells[new Vector2(2, 7)].isWall = true;
        cells[new Vector2(2, 8)].isWall = true;
        cells[new Vector2(4, 9)].isWall = true;
        cells[new Vector2(4, 8)].isWall = true;
        cells[new Vector2(4, 7)].isWall = true;
    }
    private class Cell
    {
        public Vector2 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2 connection;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}
