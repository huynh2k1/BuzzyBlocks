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
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, hexagonLayerMask);

        if(hit.collider == null)
        {
            return;
        }

        currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
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
        HoverHexaGrid(hit);
    }

    void HoverHexaGrid(RaycastHit hit)
    {
        Physics.Raycast(GetClickedRay(), out hit, 500, hexagridLayerMask);
        if (hit.collider == null)
        {
            return;
        }
        HexaCell hexaCell = hit.collider.GetComponent<HexaCell>();

        if (hexaCell != null && !hexaCell.IsOccupied)
        {
            if(targetGridCell != null)
            {
                targetGridCell.Hover(false);
                targetGridCell = null;  
            }
            targetGridCell = hexaCell;
            hexaCell.Hover(true);
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

        currentStack = null;
        targetGridCell = null;
    }

    
    private Ray GetClickedRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
}
