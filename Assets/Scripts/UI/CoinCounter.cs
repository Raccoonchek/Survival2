using TMPro;
using UnityEngine;
using YG;

public class CoinCounter : MonoBehaviour
{
    [SerializeField]private TMP_Text _textMoney;
    private int _coin = 0;

    public static CoinCounter Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _coin = YG2.saves.Coins;
        _textMoney.text = _coin.ToString();
    }

    public void AddCoin(int coins)
    {
        _coin += coins;
        YG2.saves.Coins = _coin;
        _textMoney.text = _coin.ToString();
        YG2.SaveProgress();
    }
    public void RemoveCoin(int coin)
    {
        _coin -= coin;
        YG2.saves.Coins = _coin;
        _textMoney.text = _coin.ToString();
        YG2.SaveProgress();
    }

    public int ReturnCoin()
    {
        return _coin;
    }
}
