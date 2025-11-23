using UnityEngine;

public class LevelCtrl : MonoBehaviour
{
    public Level[] listLevel;
    [SerializeField] Level levelPrefab;
    public static Level CurLevel;
    public void InitLevel()
    {
        DestroyCurrentLevel();
        Level lvl = Instantiate(listLevel[PrefData.CurLevel], transform);
        CurLevel = lvl;
        ProgressCtrl.I.InitProgress(lvl.target);
        StackSpawner.I.SpawnStacks();
    }

    public void NextLevel()
    {
        CheckIncreaseLevel();
        InitLevel();
    }

    public void CheckIncreaseLevel()
    {
        if(PrefData.CurLevel < listLevel.Length - 1)
        {
            PrefData.CurLevel++;
        }
        else
        {
            PrefData.CurLevel = 0;
        }
    }

    public void DestroyCurrentLevel()
    {
        if (CurLevel != null)
        {
            CurLevel.Destroy();
            CurLevel = null;
        }
    }
}
