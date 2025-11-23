using UnityEngine;
using UnityEngine.UI;

public class CoinCtrl : MonoBehaviour
{
    public static CoinCtrl I;
    [SerializeField] Text _text;

    private void Awake()
    {
        I = this;
    }

    private void OnEnable()
    {
        UpdateTxtCoin();
    }

    public void UpdateTxtCoin()
    {
        _text.text = PrefData.Coin.ToString();

    }
}
