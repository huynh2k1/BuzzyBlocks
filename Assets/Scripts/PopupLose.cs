using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : BasePopup
{
    public override UIType Type => UIType.Lose;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;

    public static Action OnClickReplayAction;
    public static Action OnClickHomeAction;

    protected override void Awake()
    {
        base.Awake();
        _btnReplay.onClick.AddListener(OnClickReplay);
        _btnHome.onClick.AddListener(OnClickHome);
    }

    void OnClickReplay()
    {
        Hide();
        OnClickReplayAction?.Invoke();
    }

    void OnClickHome()
    {
        Hide();
        OnClickHomeAction?.Invoke();
    }

}
