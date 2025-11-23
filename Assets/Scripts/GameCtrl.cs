using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl I;
    public StateGame State;
    [SerializeField] UICtrl uiCtrl;
    [SerializeField] LevelCtrl lvlCtrl;

    public bool isRocket;

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

        PopupWin.HomeAction += Home;
        PopupWin.NextAction += NextGame;
        PopupWin.ReplayAction += ReplayGame;

        PopupLose.HomeAction += Home;
        PopupLose.ReplayAction += ReplayGame;
    }

    private void OnDestroy()
    {
        UIHome.OnClickPlayAction -= PlayGame;

        UIGame.OnClickHomeAction -= Home;
        UIGame.OnClickReplayAction -= ReplayGame;

        PopupWin.HomeAction -= Home;
        PopupWin.NextAction -= NextGame;
        PopupWin.ReplayAction -= ReplayGame;

        PopupLose.HomeAction -= Home;
        PopupLose.ReplayAction -= ReplayGame;
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
        lvlCtrl.InitLevel();
        isRocket = false;
    }

    void ReplayGame()
    {
        State = StateGame.Playing;
        uiCtrl.ShowGame();
        lvlCtrl.InitLevel();
        isRocket = false;
    }

    void NextGame()
    {
        uiCtrl.ShowGame();
        State = StateGame.Playing;
        lvlCtrl.NextLevel();
    }

    public void WinGame()
    {
        if (State != StateGame.Playing)
            return;
        MusicCtrl.I.PlaySFXByType(TypeSFX.WIN);
        PrefData.Coin += 200;
        PrefData.BoosterShuffle += 2;
        PrefData.BoosterRocket += 1;
        State = StateGame.None;
        uiCtrl.Show(TypeUI.Win);
    }

    public void LoseGame()
    {
        MusicCtrl.I.PlaySFXByType(TypeSFX.LOSE);
        State = StateGame.None;
        uiCtrl.Show(TypeUI.Lose);
    }

    public void UseRocketBooster()
    {
        isRocket = true;
        //Show UI 
    }
}

public enum StateGame
{
    None,
    Playing,
}
