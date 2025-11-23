using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHome : UIRoot
{
    public override TypeUI Type => TypeUI.Home;
    [SerializeField] Button _btnPlay;
    [SerializeField] Button _btnHowToPlay;

    public static Action OnClickPlayAction;
    public static Action OnClickHowToPlayAction;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnClickPlay);
        _btnHowToPlay.onClick.AddListener(OnClickHowToPlay);
    }

    void OnClickPlay()
    {
        OnClickPlayAction?.Invoke();
    }

    void OnClickHowToPlay()
    {
        OnClickHowToPlayAction?.Invoke();   
    }
}
