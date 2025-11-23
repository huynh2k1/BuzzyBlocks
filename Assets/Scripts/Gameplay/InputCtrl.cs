using System;
using UnityEngine;

public class InputCtrl : MonoBehaviour
{
    [SerializeField] private LayerMask hexagonLayerMask;
    [SerializeField] private LayerMask hexagridLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] HexaStack currentStack;
    Vector3 currentStackInitPos;

    [SerializeField] HexaCell targetGridCell;

    public static Action<HexaCell> OnStackPlace;



    private void Update()
    {
        if (GameCtrl.I.State != StateGame.Playing)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        else if(Input.GetMouseButton(0) && currentStack != null)
        {
            HandleMouseDrag();
        }
        else if(Input.GetMouseButtonUp(0) && currentStack != null)
        {
            HandleMouseUp();
        }
    }


    void HandleMouseDown()
    {
        ClickHexaGrid();
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, hexagonLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        MusicCtrl.I.PlaySFXByType(TypeSFX.CLICK);
        currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        if(currentStack && GameCtrl.I.isRocket)
        {
            GameCtrl.I.isRocket = false;
        }
        currentStackInitPos = currentStack.GetPos;
    }


    void HandleMouseDrag()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, groundLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        Vector3 currentStackTargetPos = hit.point.With(y: 2);
        currentStack.transform.position = currentStackTargetPos;
        HoverHexaGrid();
    }

    void ClickHexaGrid()
    {
        if (GameCtrl.I.isRocket == false)
            return;

        if (Physics.Raycast(GetClickedRay(), out RaycastHit hit, 500, hexagridLayerMask))
        {
            HexaCell hexaCell = hit.collider.GetComponentInParent<HexaCell>();
            if (hexaCell == null)
                return;
            if (hexaCell.IsOccupied == false)
                return;
            if (hexaCell.cellType == CellType.NORMAL)
            {
                hexaCell.Stack.DestroyStack();
                GameCtrl.I.isRocket = false;
            }
        }
    }

    void HoverHexaGrid()
    {
        if (Physics.Raycast(GetClickedRay(), out RaycastHit hit, 500, hexagridLayerMask))
        {
            HexaCell hexaCell = hit.collider.GetComponentInParent<HexaCell>();

            if (hexaCell == null)
                return;

            if (hexaCell.IsOccupied || hexaCell.cellType != CellType.NORMAL)
            {
                //Tắt hover cell trước đó 
                if(targetGridCell != null)
                {
                    targetGridCell.Hover(false);
                    targetGridCell = null;  
                }
                return;
            }

            // Lần đầu hover
            if (targetGridCell == null)
            {
                targetGridCell = hexaCell;
                targetGridCell.Hover(true);
                return;
            }

            // Nếu hover sang cell khác
            if (hexaCell != targetGridCell)
            {
                targetGridCell.Hover(false);
                targetGridCell = hexaCell;
                targetGridCell.Hover(true);
            }
        }
        else
        {
            // Raycast không trúng cell nào => tắt hover cũ
            if (targetGridCell != null)
            {
                targetGridCell.Hover(false);
                targetGridCell = null;
            }
        }
    }

    void HandleMouseUp()
    {
        if(targetGridCell == null)
        {
            currentStack.transform.position = currentStackInitPos;
            currentStack = null;
            return;
        }

        currentStack.transform.position = targetGridCell.GetPos.With(y: 0.1f);
        currentStack.transform.SetParent(targetGridCell.transform);
        currentStack.Place();

        MusicCtrl.I.PlaySFXByType(TypeSFX.PLACED);

        targetGridCell.AssignStack(currentStack);
        OnStackPlace?.Invoke(targetGridCell);

        currentStack = null;

        targetGridCell.Hover(false);
        targetGridCell = null;
    }

    
    private Ray GetClickedRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
}
