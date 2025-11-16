using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUI
{
    public override UIType Type => UIType.Game;
    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;

    public static Action OnClickHomeAction;
    public static Action OnClickReplayAction;

    private void Awake()
    {
        _btnHome.onClick.AddListener(OnClickHome);
        _btnReplay.onClick.AddListener(OnClickReplay);
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
