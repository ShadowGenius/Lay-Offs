using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathfindingOptimized : MonoBehaviour
{
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private int cellWidth = 1;
    [SerializeField] private int cellHeight = 1;

    [SerializeField] private bool newPath;
    [SerializeField] private bool visualiseGrid;
    [SerializeField] private bool showTexts;

    [SerializeField] private Transform textPrefab;
    [SerializeField] private Transform textParent;

    private Dictionary<Vector2, Cell> cells;

    public List<Vector2> cellsToSearch;
    public List<Vector2> searchedCells;
    public List<Vector2> finalPath;

    bool pathGenerated;
    private void Update()
    {
        if (newPath && !pathGenerated)
        {
            GenerateGrid();

            FindPath(new Vector2(0, 0), new Vector2(6, 8));

            if (showTexts)
            {
                VisualiseText();
            }

            pathGenerated = true;
        }
        else if (!newPath)
        {
            pathGenerated = false;
        }
    }

    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();

        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }
        for (int i = 0; i < 40; i++)
        {
            Vector2 pos = new Vector2(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
            cells[pos].isWall = true;
        }
    }


    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        cellsToSearch = new List<Vector2> { startPos };
        searchedCells = new List<Vector2>();
        finalPath = new List<Vector2>();

        cells[startPos].gCost = 0;
        cells[startPos].hCost = GetDistance(startPos, endPos);
        cells[startPos].fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];

            foreach (Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if (c.fCost < cells[cellToSearch].fCost ||
                    c.fCost == cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }


            cellsToSearch.Remove(cellToSearch);
            searchedCells.Add(cellToSearch);

            if (cellToSearch == endPos)
            {
                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    finalPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                finalPath.Add(startPos);
                VisualiseText();
                return;
            }

            SearchCellNeighbors(cellToSearch, endPos);
        }

        if (finalPath.Count == 0)
        {
            Debug.Log("Path not found");
        }
    }

    private void VisualiseText()
    {
        foreach (Transform child in textParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Vector2 pos in cells.Keys)
        {
            Transform text = Instantiate(textPrefab, pos + (Vector2)transform.position, new Quaternion(), textParent);
            text.GetChild(0).GetComponent<Text>().text = cells[pos].gCost.ToString();
            text.GetChild(1).GetComponent<Text>().text = cells[pos].hCost.ToString();
            text.GetChild(2).GetComponent<Text>().text = cells[pos].fCost.ToString();
        }
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbour = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbour < cells[neighborPos].gCost)
                    {
                        Cell neighbourNode = cells[neighborPos];

                        neighbourNode.connection = cellPos;
                        neighbourNode.gCost = GcostToNeighbour;
                        neighbourNode.hCost = GetDistance(neighborPos, endPos);
                        neighbourNode.fCost = neighbourNode.gCost + neighbourNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private int GetDistance(Vector2 pos1, Vector2 pos2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);
        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;

    }

    private void OnDrawGizmos()
    {
        if (!visualiseGrid || cells == null)
        {
            return;
        }

        foreach (KeyValuePair<Vector2, Cell> kvp in cells)
        {
            if (!kvp.Value.isWall)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }

            if (finalPath.Contains(kvp.Key))
            {
                Gizmos.color = Color.magenta;
            }

            float gizmoSize = showTexts ? 0.2f : 1;

            Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth, cellHeight) * gizmoSize);
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
