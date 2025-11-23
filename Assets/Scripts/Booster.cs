using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    public Type type;
    public int count;
    [SerializeField] Button _btn;
    [SerializeField] Text _txtPrice;
    [SerializeField] Text _txtCount;

    const int price = 200;

    private void Awake()
    {
        _btn.onClick.AddListener(OnClickThis);
    }

    public void Initialze()
    {
        LoadCountRemaining();
        CheckShowTxtPrice();
    }

    void LoadCountRemaining()
    {
        switch (type)
        {
            case Type.Shuffle:
                count = PrefData.BoosterShuffle;
                break;
            case Type.Rocket:
                count = PrefData.BoosterRocket;
                break;
        }
        UpdateTxtCount();
    }

    void SaveCountRemaining()
    {
        switch (type)
        {
            case Type.Shuffle:
                PrefData.BoosterShuffle = count;
                break;
            case Type.Rocket:
                PrefData.BoosterRocket = count;
                break;
        }
    }

    void UpdateTxtCount()
    {
        _txtCount.text = count.ToString();
    }

    public void OnClickThis()
    {
        if(count <= 0)
        {
            if(PrefData.Coin >= price)
            {
                PrefData.Coin -= price;
                CoinCtrl.I.UpdateTxtCoin();
                UpdateTextPrice();
                HandleActionBooster();
            }
            return;
        }

        count--;
        SaveCountRemaining();
        UpdateTxtCount();
        CheckShowTxtPrice();
        HandleActionBooster();
    }

    void HandleActionBooster()
    {
        switch (type)
        {
            case Type.Shuffle:
                StackSpawner.I.SpawnStacks();
                break;
            case Type.Rocket:
                GameCtrl.I.UseRocketBooster();
                break;
        }
    }

    public void CheckShowTxtPrice()
    {
        if (count == 0)
        {
            _txtPrice.gameObject.SetActive(true);
            UpdateTextPrice();
        }
        else
        {
            _txtPrice.gameObject.SetActive(false);
        }
    }

    public void UpdateTextPrice()
    {
        if(PrefData.Coin >= 200)
        {
            _txtPrice.color = Color.white;
        }
        else
        {
            _txtPrice.color = Color.red;
        }
    }

    public enum Type
    {
        Shuffle,
        Rocket,
    }
}
