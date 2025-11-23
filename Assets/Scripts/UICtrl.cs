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
        Show(TypeUI.Home);
        Hide(TypeUI.Game);
    }

    public void ShowGame()
    {
        Show(TypeUI.Game);
        Hide(TypeUI.Home);
    }

    void ShowHTP()
    {
        Show(TypeUI.HowToPlay);
    }
}
