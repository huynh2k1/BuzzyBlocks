using UnityEngine;

public static class PrefData 
{
    public const string CURLEVEL = "CURLEVEL";
    public const string LEVELUNLOCK = "LEVELUNLOCK";

    public const string COIN = "COIN";
    public const string SOUND = "SOUND";
    public const string MUSIC = "MUSIC";

    public const string BOOSTERSHUFFLE = "BOOSTERSHUFFLE";
    public const string BOOSTERROCKET = "BOOSTERROCKET";
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt(CURLEVEL, 0);
        set => PlayerPrefs.SetInt(CURLEVEL, value); 
    }

    public static int LevelUnlock
    {
        get => PlayerPrefs.GetInt(LEVELUNLOCK, 0);
        set => PlayerPrefs.SetInt(LEVELUNLOCK, value);
    }

    public static int BoosterShuffle
    {
        get => PlayerPrefs.GetInt(BOOSTERSHUFFLE, 0);
        set => PlayerPrefs.SetInt(BOOSTERSHUFFLE, value);
    }

    public static int BoosterRocket
    {
        get => PlayerPrefs.GetInt(BOOSTERROCKET, 0);
        set => PlayerPrefs.SetInt(BOOSTERROCKET, value);
    }

    public static int Coin
    {
        get => PlayerPrefs.GetInt(COIN, 0);
        set => PlayerPrefs.SetInt(COIN, value);
    }

    public static bool Sound
    {
        get => PlayerPrefs.GetInt(SOUND, 0) == 0 ? true : false;
        set => PlayerPrefs.SetInt(SOUND, value ? 0 : 1);
    }

    public static bool Music
    {
        get => PlayerPrefs.GetInt(MUSIC, 0) == 0 ? true : false;
        set => PlayerPrefs.SetInt(MUSIC, value ? 0 : 1);
    }
}
