using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : RootPopup
{
    public override TypeUI Type => TypeUI.Lose;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;

    [SerializeField] Text _txtCoin;

    public static Action ReplayAction;
    public static Action HomeAction;

    protected override void Awake()
    {
        base.Awake();
        _btnReplay.onClick.AddListener(OnClickReplay);
        _btnHome.onClick.AddListener(OnClickHome);
    }

    public override void Active()
    {
        base.Active();
        UpdateTextCoin();
    }

    void UpdateTextCoin()
    {
        _txtCoin.text = PrefData.Coin.ToString();
    }

    public override void DeActive()
    {
        base.DeActive();
    }
    void OnClickReplay()
    {
        DeActive();
        ReplayAction?.Invoke();
    }

    void OnClickHome()
    {
        DeActive();
        HomeAction?.Invoke();
    }

}
