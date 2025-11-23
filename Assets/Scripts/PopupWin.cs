using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : RootPopup
{
    public override TypeUI Type => TypeUI.Win;

    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnNext;

    public static Action ReplayAction;
    public static Action HomeAction;
    public static Action NextAction;

    protected override void Awake()
    {
        base.Awake();
        _btnReplay.onClick.AddListener(ReplayEvent);
        _btnHome.onClick.AddListener(HomeEvent);
        _btnNext.onClick.AddListener(NextEvent);
    }

    void ReplayEvent()
    {
        DeActive();
        ReplayAction?.Invoke();
    }

    void HomeEvent()
    {
        DeActive();
        HomeAction?.Invoke();
    }

    void NextEvent()
    {
        DeActive();
        NextAction?.Invoke();
    }
}
