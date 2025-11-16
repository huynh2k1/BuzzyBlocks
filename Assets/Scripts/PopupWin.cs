using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : BasePopup
{
    public override UIType Type => UIType.Win;

    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnNext;

    public static Action OnClickReplayAction;
    public static Action OnClickHomeAction;
    public static Action OnClickNextAction;

    protected override void Awake()
    {
        base.Awake();
        _btnReplay.onClick.AddListener(OnClickReplay);
        _btnHome.onClick.AddListener(OnClickHome);
        _btnNext.onClick.AddListener(OnClickNext);
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

    void OnClickNext()
    {
        Hide();
        OnClickNextAction?.Invoke();
    }
}
