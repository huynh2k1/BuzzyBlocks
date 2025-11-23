using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIRoot
{
    public override TypeUI Type => TypeUI.Game;
    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;
    [SerializeField] Booster[] _boosters;

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
        CoinCtrl.I.UpdateTxtCoin();
        foreach(var b in _boosters)
        {
            b.Initialze();
        }
    }

    void OnClickHome()
    {
        OnClickHomeAction?.Invoke();
    }
    void OnClickReplay()
    {
        OnClickReplayAction?.Invoke();
    }


}
