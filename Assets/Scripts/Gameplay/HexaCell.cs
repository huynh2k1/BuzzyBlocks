using TMPro;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public HexaStack Stack { get; private set; } //Stack chiếm đóng
    public Vector3 GetPos => transform.position;

    public int q;
    public int r;

    public int quantityUnlock;

    public CellType cellType;

    [SerializeField] TMP_Text txtNumUnlock;

    [SerializeField] GameObject lockObj;
    [SerializeField] MeshRenderer _renderer;
    [SerializeField] Material _normal, _hover;

    public bool IsOccupied
    {
        get => Stack != null;
        private set { }
    }

    public void Hover(bool isHover)
    {
        var mats = _renderer.materials;  // lấy ra mảng copy
        mats[1] = isHover ? _hover : _normal;
        _renderer.materials = mats;
    }

    void UpdateTxtNumUnlock(int quantity)
    {
        txtNumUnlock.text = quantity.ToString();
    }

    void CheckQuantityRemaining(int number)
    {
        if(number >= quantityUnlock)
        {
            //Unlock
            ShowLock(false);
        }
        else
        {
            //Lock
        }
    }

    void ShowLock(bool isShow, int quantity = 0)
    {
        if (isShow)
        {
            UpdateTxtNumUnlock(quantity);
        }
        lockObj.SetActive(isShow);  
    }

    public void AssignStack(HexaStack stack)
    {
        Stack = stack;
    }
}
public enum CellType
{
    EMPTY,
    NORMAL,
    LOCK10,
    LOCK20,
    LOCK30,
    LOCK50,
}