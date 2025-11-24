using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl I;
    public StateGame State;
    [SerializeField] UICtrl uiCtrl;
    [SerializeField] LevelCtrl lvlCtrl;
    [SerializeField] UIGame _uiGame;
    public bool isRocket;

    [SerializeField] Booster[] _boosters;
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
        UIHome.OnClickPlayAction += InitGame;

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
        UIHome.OnClickPlayAction -= InitGame;

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

    void InitGame()
    {
        State = StateGame.Playing;
        uiCtrl.ShowGame();
        lvlCtrl.InitLevel();
        isRocket = false;
        InitBooster();
    }

    void ReplayGame()
    {
        InitGame();
    }

    void NextGame()
    {
        lvlCtrl.CheckIncreaseLevel();

        InitGame();
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

    //BOOSTER
    public void InitBooster()
    {
        foreach (var b in _boosters)
        {
            b.Initialze();
        }
    }
    public void UseRocketBooster(bool isUse)
    {
        isRocket = isUse;
        _uiGame.ShowTutRocket(isUse);
        //Show UI 
    }

    public void OnBoosterUseSuccess(Booster.Type type)
    {
        foreach(var b in _boosters)
        {
            if(b.type == type)
            {
                b.HandleUseSuccess();
            }
        }
    }
}

public enum StateGame
{
    None,
    Playing,
}
