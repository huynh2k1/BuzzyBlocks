using TMPro;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public int q;
    public int r;

    public int quantityUnlock;

    public CellType cellType;

    [SerializeField] TMP_Text txtNumUnlock;

    [SerializeField] GameObject lockObj;

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