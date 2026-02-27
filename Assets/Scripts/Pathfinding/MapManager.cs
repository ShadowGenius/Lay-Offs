using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{

    [SerializeField] private int gridHeight = 10;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private float cellHeight = 1f;
    [SerializeField] private float cellWidth = 1f;
    [SerializeField] private Vector2 offset = new Vector2(0, 0);
    [SerializeField] private bool visualizeGrid;
    [SerializeField] private Tilemap wallTiles;

    private Dictionary<Vector2Int, Cell> cells;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2Int, Cell>();

        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 wpos = new Vector2(x, y);
                Vector2Int pos = findGridPosReg(wpos);
                cells.Add(pos, new Cell(pos));
                Debug.Log(pos);
                
            }
        }
        Debug.Log($"Generated grid with {cells.Count} cells.");
        /*cells[new Vector2(2, 0)].isWall = true;
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
        cells[new Vector2(4, 7)].isWall = true;*/
    }
    public List<Vector2> FindPath(Vector2 startPosWorld, Vector2 endPosWorld)
    {
        Debug.Log("StartPosWorld gotten" + startPosWorld);
        Vector2Int startPos = findGridPos(startPosWorld);
        Vector2Int endPos = findGridPos(endPosWorld);
        Dictionary<Vector2Int, Path> pathNodes = new Dictionary<Vector2Int, Path>();
        
        foreach (var kvp in cells)
        {
            pathNodes.Add(kvp.Key, new Path(kvp.Key));
        }

        /**if (!pathNodes.ContainsKey(startPos) || !pathNodes.ContainsKey(endPos))
        {
            Debug.Log("MM returns null 2");
            return null;
        }*/
        if (!pathNodes.ContainsKey(startPos))
        {
            Debug.Log($"Start position {startPos} does not exist in the grid.");
            return null;
        }
        List<Vector2Int> searchedCells = new List<Vector2Int>();
        List<Vector2Int> cellsToSearch = new List<Vector2Int> { startPos };
        List<Vector2Int> finalPath = new List<Vector2Int>();

        Path startNode = pathNodes[startPos];

        startNode.gCost = 0;
        startNode.hCost = GetDistance(startPos, endPos);
        startNode.fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2Int currentCell = cellsToSearch[0];

            foreach (Vector2Int pos in cellsToSearch)
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
                List<Vector2> npcPath = new List<Vector2>();
                while (pathNode.position != startPos)
                {
                    Vector3 world = wallTiles.GetCellCenterWorld(new Vector3Int(pathNode.position.x, pathNode.position.y, 0));
                    npcPath.Add(world);
                    pathNode = pathNodes[pathNode.connection];
                }
                Vector3 startPosW = wallTiles.GetCellCenterWorld(new Vector3Int(startPos.x, startPos.y, 0));
                npcPath.Add(startPosW);
                npcPath.Reverse();
                Debug.Log("MM returns good");
                return npcPath;
            }
            SearchCellNeighbors(currentCell, endPos, pathNodes, cellsToSearch, searchedCells);
        }
        Debug.Log("MM returns null");
        return null;
    }


    private void SearchCellNeighbors(Vector2Int cellPos, Vector2 endPos, Dictionary<Vector2Int, Path> pathNodes, List<Vector2Int> cellsToSearch, List<Vector2Int> searchedCells)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPosWorld = new Vector2(x, y);
                Vector2Int neighborPos = findGridPos(neighborPosWorld);
                if (neighborPos == cellPos)
                {
                    continue;
                }
                if(!cells.ContainsKey(neighborPos))
                {
                    continue;
                }
                if (isWall(neighborPos))
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
        foreach (KeyValuePair<Vector2Int, Cell> kvp in cells)
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
        public Vector2Int position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2Int connection;
        public bool isWall;

        public Path(Vector2Int pos)
        {
            position = pos;
        }
    }

    private Vector2Int findGridPos(Vector2 pos)
    {
        Vector3Int cell = wallTiles.WorldToCell(pos - offset);
        return new Vector2Int(cell.x, cell.y);
    }

    private Vector2Int findGridPosReg(Vector2 pos)
    {
        Vector3Int cell = wallTiles.WorldToCell(pos);
        return new Vector2Int(cell.x, cell.y);
    }

    private bool isWall(Vector2Int pos)
    {
        return wallTiles.HasTile(new Vector3Int(pos.x, pos.y, 0));
    }
}
