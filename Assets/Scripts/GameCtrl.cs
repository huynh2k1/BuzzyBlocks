using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl I;
    [SerializeField] UICtrl uiCtrl;
    public StateGame State;
    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        Home();
    }

    private void OnEnable()
    {
        UIHome.OnClickPlayAction += PlayGame;

        UIGame.OnClickHomeAction += Home;
        UIGame.OnClickReplayAction += ReplayGame;

        PopupWin.OnClickHomeAction += Home;
        PopupWin.OnClickNextAction += NextGame;
        PopupWin.OnClickReplayAction += ReplayGame;

        PopupLose.OnClickHomeAction += Home;
        PopupLose.OnClickReplayAction += ReplayGame;
    }

    private void OnDisable()
    {
        
    }

    void Home()
    {
        State = StateGame.None;
        uiCtrl.ShowHome();
    }

    void PlayGame()
    {
        State = StateGame.Playing;
        uiCtrl.ShowGame();
    }

    void ReplayGame()
    {
        State = StateGame.Playing;
    }

    void NextGame()
    {
        State = StateGame.Playing;

    }

    void WinGame()
    {
        State = StateGame.None;
        uiCtrl.Show(UIType.Win);
    }

    void LoseGame()
    {
        State = StateGame.None;
        uiCtrl.Show(UIType.Lose);
    }
}

public enum StateGame
{
    None,
    Playing,
}
