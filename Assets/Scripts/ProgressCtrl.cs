using UnityEngine;
using UnityEngine.UI;

public class ProgressCtrl : MonoBehaviour
{
    public static ProgressCtrl I;
    [SerializeField] Text _textProgress;
    int maxProgress;
    int curProgress;

    private void Awake()
    {
        I = this;
    }

    public void InitProgress(int max)
    {
        curProgress = 0;  
        maxProgress = max;
        UpdateTextProgress();
    }

    public void UpdateProgress(int progress)
    {
        curProgress += progress;
        LevelCtrl.CurLevel.UpdateLockCells(curProgress);
        UpdateTextProgress();
    }

    public void CheckWin()
    {
        if (curProgress >= maxProgress)
        {
            GameCtrl.I.WinGame();
        }
    }

    public void UpdateTextProgress()
    {
        _textProgress.text = $"{curProgress.ToString("00")}/{maxProgress.ToString("00")}";
    }
}
