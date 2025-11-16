using UnityEngine;

public static class DataPlayer
{
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt("CurLevel", 0);
        set => PlayerPrefs.SetInt("CurLevel", value);
    }

}
