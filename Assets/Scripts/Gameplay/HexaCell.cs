using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public CellType cellType;
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