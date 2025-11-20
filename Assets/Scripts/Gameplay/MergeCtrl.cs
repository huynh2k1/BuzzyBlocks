using UnityEngine;
using System.Collections.Generic;

public class MergeCtrl : MonoBehaviour
{
    [SerializeField] HexaGridCtrl gridCtrl;

    List<HexaCell> updateCells = new List<HexaCell>();

    private void Awake()
    {
    }



    void StackPlacedCallBack(HexaCell cellPlaced)
    {

    }
}
