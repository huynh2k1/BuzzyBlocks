using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem.XInput;

public class MergeCtrl : MonoBehaviour
{
    [SerializeField] Level gridCtrl;

    //danh sách các hexacell cần kiểm tra để merge (hàng đợi)
    private List<HexaCell> updateCells = new List<HexaCell>();

    private void Awake()
    {
        InputCtrl.OnStackPlace += StackPlacedCallBack;
    }

    private void OnDestroy()
    {
        InputCtrl.OnStackPlace -= StackPlacedCallBack;
    }

    void StackPlacedCallBack(HexaCell cellPlaced)
    {
        StartCoroutine(StackPlacedCoroutine(cellPlaced));
    }

    IEnumerator StackPlacedCoroutine(HexaCell cellPlaced)
    {
        updateCells.Add(cellPlaced);
        while (updateCells.Count > 0)
            yield return CheckForMerge(updateCells[0]);

        if (gridCtrl.IsGameOver())
        {
            GameCtrl.I.LoseGame();
        }
    }

    IEnumerator CheckForMerge(HexaCell cellPlaced)
    {
        updateCells.Remove(cellPlaced);

        if (!cellPlaced.IsOccupied)
            yield break;

        //Kiểm tra các stack lân cận ?
        List<HexaCell> neightbors = gridCtrl.GetNeighbors(cellPlaced);
        if (neightbors.Count <= 0)
            yield break;

        //Lấy ra màu của hexa top
        Color topColor = cellPlaced.Stack.GetTopHexagonColor();

        //Kiểm tra phần tử trên cùng của các stack lân cận có cùng màu ?
        List<HexaCell> similarNeightborTopColor = GetSimilarNeightborHexagrid(topColor, neightbors);
        if (similarNeightborTopColor.Count <= 0)
            yield break;

        updateCells.AddRange(similarNeightborTopColor);

        //Lấy danh sách các hexagon có thể add
        List<Hexagon> hexagonsToAdd = GetHexagonsToAdd(topColor, similarNeightborTopColor);

        //Remove the hexagons from their stacks
        RemoveHexagonsFromTheirStacks(similarNeightborTopColor, hexagonsToAdd.ToArray());

        //Move Hexagons To Cur Stack
        MoveHexagons(cellPlaced, hexagonsToAdd, 0.1f);
        yield return new WaitForSeconds(0.3f + hexagonsToAdd.Count * 0.05f);
        //Merge
        yield return CheckForCompleteStack(cellPlaced, topColor);

    }

    List<HexaCell> GetSimilarNeightborHexagrid(Color topColor, List<HexaCell> neightbors)
    {
        List<HexaCell> similarNeightborTopColor = new List<HexaCell>();

        foreach (HexaCell hexa in neightbors)
        {
            Color neightborHexaGridTopHexagonColor = hexa.Stack.GetTopHexagonColor();

            if (neightborHexaGridTopHexagonColor == topColor)
                similarNeightborTopColor.Add(hexa);
        }
        return similarNeightborTopColor;
    }

    List<Hexagon> GetHexagonsToAdd(Color topColor, List<HexaCell> similarNeightborTopColor)
    {
        List<Hexagon> hexagonsToAdd = new List<Hexagon>();
        foreach (HexaCell neightbor in similarNeightborTopColor)
        {
            HexaStack hexaStack = neightbor.Stack;
            for (int i = hexaStack.Hexagons.Count - 1; i >= 0; i--)
            {
                Hexagon hexagon = hexaStack.Hexagons[i];
                if (hexagon.Color != topColor)
                    break;

                hexagonsToAdd.Add(hexagon);
                hexagon.SetParent(null);
            }
        }
        return hexagonsToAdd;
    }

    void RemoveHexagonsFromTheirStacks(List<HexaCell> similarNeightborTopColor, Hexagon[] hexagonsToAdd)
    {
        foreach (HexaCell neightbor in similarNeightborTopColor)
        {
            HexaStack stack = neightbor.Stack;

            foreach (Hexagon hexagon in hexagonsToAdd)
            {
                if (stack.Contains(hexagon))
                {
                    stack.Remove(hexagon);
                }
            }
        }
    }

    void MoveHexagons(HexaCell cellPlaced, List<Hexagon> hexagonsToAdd, float heightHexa = 0.1f)
    {
        //Tính vị trí y của stack placed
        //0.2f: là chiều cao của 1 hexagon
        float initY = cellPlaced.Stack.Hexagons.Count * heightHexa;

        for (int i = 0; i < hexagonsToAdd.Count; i++)
        {
            Hexagon hexagon = hexagonsToAdd[i];

            float targetY = initY + i * heightHexa;
            Vector3 targetLocalPos = Vector3.up * targetY;

            cellPlaced.Stack.Add(hexagon);
            hexagon.MoveToLocal(targetLocalPos);
        }
    }

    IEnumerator CheckForCompleteStack(HexaCell cellPlaced, Color topColor)
    {
        if (cellPlaced.Stack.Hexagons.Count < 10)
            yield break;

        List<Hexagon> similarHexagons = new List<Hexagon>();

        for (int i = cellPlaced.Stack.Hexagons.Count - 1; i >= 0; i--)
        {
            Hexagon hexagon = cellPlaced.Stack.Hexagons[i];

            if (hexagon.Color != topColor)
                break;

            similarHexagons.Add(hexagon);
        }
        int similarHexagonCount = similarHexagons.Count;
        if (similarHexagons.Count < 10)
            yield break;

        float delay = 0;

        while (similarHexagons.Count > 0)
        {
            similarHexagons[0].SetParent(null);
            similarHexagons[0].Vanish(delay);
            delay += 0.05f;

            //DestroyImmediate(similarHexagons[0].gameObject);
            cellPlaced.Stack.Remove(similarHexagons[0]);
            similarHexagons.RemoveAt(0);
        }

        updateCells.Add(cellPlaced);

        yield return new WaitForSeconds(0.3f + similarHexagonCount * 0.01f);
        ProgressCtrl.I.UpdateProgress(similarHexagonCount);
        ProgressCtrl.I.CheckWin();
    }
}
