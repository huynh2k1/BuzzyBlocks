using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float hexSize = 0.8659765f;
    [SerializeField] HexaCell hexaCell;
    [SerializeField] Grid grid;

    [Header("MISSION")]
    public int target;
    private Dictionary<(int, int), HexaCell> dictGrid = new Dictionary<(int, int), HexaCell>();

    private static readonly Vector3Int[] evenRow =
    {
        new Vector3Int(+1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(-1, +1, 0),
        new Vector3Int(0, +1, 0)
    };

    private static readonly Vector3Int[] oddRow =
    {
        new Vector3Int(+1, 0, 0),
        new Vector3Int(+1, -1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, +1, 0),
        new Vector3Int(+1, +1, 0)
    };

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        HexaCell[] childs = GetComponentsInChildren<HexaCell>();

        foreach(HexaCell c in childs)
        {
            c.Initialize(grid);
            dictGrid[(c.x, c.y)] = c;
        }
    }

    public List<HexaCell> GetNeighbors(HexaCell cell)
    {
        bool isOdd = (cell.y & 1) == 1;

        Vector3Int[] dirs = isOdd ? oddRow : evenRow;
        List<HexaCell> result = new List<HexaCell>();

        foreach (var dir in dirs)
        {
            int x = cell.x + dir.x;
            int y = cell.y + dir.y;

            if (dictGrid.ContainsKey((x, y)))
            {
                if (dictGrid[(x, y)].IsOccupied == false)
                    continue;
                result.Add(dictGrid[(x, y)]);
            }
        }

        return result;
    }

    public void UpdateLockCells(int quantity)
    {
        foreach(var c in dictGrid.Values)
        {
            if(c.cellType != CellType.NORMAL)
            {
                c.CheckQuantityRemaining(quantity);
            }
        }
    }

    bool CanPlace(HexaCell cell)
    {
        if (cell.IsOccupied)
            return false;
        return true;
    }

    public bool IsGameOver()
    {
        foreach(var cell in dictGrid.Values)
        {
            if (CanPlace(cell))
                return false;
        }
        return true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
