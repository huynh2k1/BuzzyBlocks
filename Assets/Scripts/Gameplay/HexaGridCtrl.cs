using System.Collections.Generic;
using UnityEngine;

public class HexaGridCtrl : MonoBehaviour
{
    public int Size = 4;
    [SerializeField] float hexSize = 0.8659765f;
    [SerializeField] GameObject hexPrefab;
    [SerializeField] List<GameObject> spawned = new List<GameObject>();
    private readonly float sqrt3 = 1.73205f;

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

                Instantiate(hexPrefab, pos, Quaternion.identity, transform)
                    .name = $"Hex_{q}_{r}";
            }
        }
    }

    Vector3 AxialToWorld(int q, int r)
    {
        float x = hexSize * sqrt3 * (q + r * 0.5f);
        float z = hexSize * 1.5f * r;
        return new Vector3(x, 0, z);
    }
}
