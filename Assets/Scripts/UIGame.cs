using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIRoot
{
    public override TypeUI Type => TypeUI.Game;
    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;
    [SerializeField] GameObject _tutRocket;
    [SerializeField] Text _txtLevel;

    public static Action OnClickHomeAction;
    public static Action OnClickReplayAction;

    private void Awake()
    {
        _btnHome.onClick.AddListener(OnClickHome);
        _btnReplay.onClick.AddListener(OnClickReplay);
    }

    public override void Active()
    {
        base.Active();
        ShowTutRocket(false);
        CoinCtrl.I.UpdateTxtCoin();
        UpdateTextLevel();
    }

    void UpdateTextLevel()
    {
        _txtLevel.text = $"Level {PrefData.CurLevel + 1}";
    }

    void OnClickHome()
    {
        OnClickHomeAction?.Invoke();
    }
    void OnClickReplay()
    {
        OnClickReplayAction?.Invoke();
    }

    public void ShowTutRocket(bool isShow)
    {
        _tutRocket.SetActive(isShow);
    }
}
