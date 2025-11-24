using TMPro;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public HexaStack Stack { get; private set; } //Stack chiếm đóng
    public Vector3 GetPos => transform.position;
    public Vector3Int gridPos;
    public int y;
    public int x;

    public int quantityUnlock;
    int quantityRemaining;

    public CellType cellType;

    [SerializeField] TMP_Text txtNumUnlock;

    [SerializeField] GameObject lockObj;
    [SerializeField] MeshRenderer _renderer;
    [SerializeField] Material _normal, _hover;

    //Effect
    [SerializeField] ParticleSystem _effect;
    [SerializeField] AudioSource _audioSource;

    public bool IsOccupied
    {
        get => Stack != null;
        private set { }
    }


    public void Initialize(Grid grid)
    {
        gridPos = grid.WorldToCell(transform.position);
        x = gridPos.x;
        y = gridPos.y;
        name = $"( {x}, {y})";
        LoadType();
    }

    public void LoadType()
    {
        switch (cellType)
        {
            case CellType.EMPTY:
                ShowLock(false);
                break;
            case CellType.NORMAL:
                ShowLock(false);
                break;
            case CellType.LOCK10:
                quantityUnlock = 10;
                ShowLock(true);
                break;
            case CellType.LOCK20:
                quantityUnlock = 20;
                ShowLock(true);
                break;
            case CellType.LOCK30:
                quantityUnlock = 30;
                ShowLock(true);    
                break;
            case CellType.LOCK50:
                quantityUnlock = 50;
                ShowLock(true);    
                break;
            case CellType.LOCK100:
                quantityUnlock = 100;
                ShowLock(true);
                break;
            case CellType.LOCK150:
                quantityUnlock = 150;
                ShowLock(true);
                break;
        }
        quantityRemaining = quantityUnlock;
        UpdateTxtNumUnlock();

    }

    public void Hover(bool isHover)
    {
        var mats = _renderer.materials;  // lấy ra mảng copy
        mats[1] = isHover ? _hover : _normal;
        _renderer.materials = mats;
    }

    void UpdateTxtNumUnlock()
    {
        txtNumUnlock.text = quantityRemaining.ToString();
    }

    public void CheckQuantityRemaining(int number)
    {
        if(number >= quantityUnlock)
        {
            //Unlock
            PlayEffect();
            cellType = CellType.NORMAL;
            ShowLock(false);
        }
        else
        {
            quantityRemaining = quantityUnlock - number;
            UpdateTxtNumUnlock();
            //Lock
        }
    }

    void ShowLock(bool isShow)
    {
        lockObj.SetActive(isShow);  
    }

    public void AssignStack(HexaStack stack)
    {
        Stack = stack;
    }

    public void PlayEffect()
    {
        _effect.Play();
        _audioSource.Play();
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
    LOCK100,
    LOCK150,
}