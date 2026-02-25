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

    [SerializeField] private bool visualizeGrid;

    private Dictionary<Vector2, Cell> cells;

    private void Awake()
    {
        GenerateGrid();
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
    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        Dictionary<Vector2, Path> pathNodes = new Dictionary<Vector2, Path>();
        
        foreach (var kvp in cells)
        {
            pathNodes.Add(kvp.Key, new Path(kvp.Key));
        }

        List<Vector2> searchedCells = new List<Vector2>();
        List<Vector2> cellsToSearch = new List<Vector2> { startPos };
        List<Vector2> finalPath = new List<Vector2>();

        Path startNode = pathNodes[startPos];
        startNode.gCost = 0;
        startNode.hCost = GetDistance(startPos, endPos);
        startNode.fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 currentCell = cellsToSearch[0];

            foreach (Vector2 pos in cellsToSearch)
            {
                Path c = pathNodes[pos];
                Path best = pathNodes[currentCell];
                if (c.fCost < best.fCost || (c.fCost == best.fCost && c.hCost < best.hCost))
                {
                    currentCell = pos;
                }
            }

            cellsToSearch.Remove(currentCell);
            searchedCells.Add(currentCell);

            if (currentCell == endPos)
            {
                Path pathNode = pathNodes[endPos];

                while (pathNode.position != startPos)
                {
                    finalPath.Add(pathNode.position);
                    pathNode = pathNodes[pathNode.connection];
                }

                finalPath.Add(startPos);
                finalPath.Reverse();
                return finalPath;
            }
            SearchCellNeighbors(currentCell, endPos, pathNodes, cellsToSearch, searchedCells);
        }
        return null;
    }


    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos, Dictionary<Vector2, Path> pathNodes, List<Vector2> cellsToSearch, List<Vector2> searchedCells)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);
                if (neighborPos == cellPos)
                {
                    continue;
                }
                if(!cells.ContainsKey(neighborPos))
                {
                    continue;
                }
                if(cells[neighborPos].isWall)
                {
                    continue;
                }
                if(searchedCells.Contains(neighborPos))
                {
                    continue;
                }
                //if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                //{
                    int GcostToNeighbor = pathNodes[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbor < pathNodes[neighborPos].gCost)
                    {
                        Path neighborNode = pathNodes[neighborPos];

                        neighborNode.connection = cellPos;
                        neighborNode.gCost = GcostToNeighbor;
                        neighborNode.hCost = GetDistance(neighborPos, endPos);
                        neighborNode.fCost = neighborNode.gCost + neighborNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                //    cvv }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!visualizeGrid || cells == null)
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
            Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth, cellHeight));
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
    private class Cell
    {
        public Vector2 position;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
    private class Path
    {
        public Vector2 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2 connection;
        public bool isWall;

        public Path(Vector2 pos)
        {
            position = pos;
        }
    }
}
