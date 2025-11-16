using UnityEngine;

public class UICtrl : BaseUICtrl
{
    private void OnEnable()
    {
        UIHome.OnClickHowToPlayAction += ShowHTP;

    }

    private void OnDestroy()
    {
        UIHome.OnClickHowToPlayAction -= ShowHTP;
    }

    public void ShowHome()
    {
        Show(UIType.Home);
        Hide(UIType.Game);
    }

    public void ShowGame()
    {
        Show(UIType.Game);
        Hide(UIType.Home);
    }

    void ShowHTP()
    {
        Show(UIType.HowToPlay);
    }
}
