using System.Collections.Generic;
using UnityEngine;

public class HexaGridCtrl : MonoBehaviour
{
    public int Size = 4;
    [SerializeField] float hexSize = 0.8659765f;
    [SerializeField] HexaCell hexaCell;
    private Dictionary<(int, int), HexaCell> grid = new Dictionary<(int, int), HexaCell>();

    private readonly float sqrt3 = 1.73205f;
    private static readonly Vector2Int[] directions = new Vector2Int[]
{
    new Vector2Int(+1, 0),
    new Vector2Int(+1, -1),
    new Vector2Int(0, -1),
    new Vector2Int(-1, 0),
    new Vector2Int(-1, +1),
    new Vector2Int(0, +1)
    };


    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for(int q = -Size; q <= Size; ++q)
        {
            int rMin = Mathf.Max(-Size, -q - Size);
            int rMax = Mathf.Min(Size, -q + Size);

            for (int r = rMin; r <= rMax; r++)
            {
                Vector3 pos = AxialToWorld(q, r);

                HexaCell obj = Instantiate(hexaCell, pos, Quaternion.identity, transform);
                obj.name = $"Hex_{q}_{r}";
                obj.q = q;
                obj.r = r;
                grid[(q, r)] = obj;
            }
        }
    }

    Vector3 AxialToWorld(int q, int r)
    {
        float x = hexSize * sqrt3 * (q + r * 0.5f);
        float z = hexSize * 1.5f * r;
        return new Vector3(x, 0, z);
    }
    public List<HexaCell> GetNeighbors(int q, int r)
    {
        List<HexaCell> result = new List<HexaCell>();

        foreach (var dir in directions)
        {
            int nq = q + dir.x;
            int nr = r + dir.y;

            if (grid.ContainsKey((nq, nr)))
            {
                if (grid[(nq, nr)].IsOccupied == false)
                    result.Add(grid[(nq, nr)]);
            }
        }

        return result;
    }

}
