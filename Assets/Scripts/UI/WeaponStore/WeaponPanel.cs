using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponPanel : MonoBehaviour
{
    public WeaponPriceInfo WeaponPriceInfo;

    [SerializeField] private Image _imageWeapon;
    [SerializeField] private int _priceWeapon;
    [SerializeField] private TMP_Text _nameByeText;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _countCardsText;
    [SerializeField] private TMP_Text _priceWeaponText;
    [SerializeField] private int _lvlWeapon = 0;
    [SerializeField] private int _countCardsWeaponMax = 2;
    [SerializeField] private int _countCardsWeapon = 0;


    [SerializeField] private int _StreghtWeapon;
    [SerializeField] private int _AggilityWeapon;
    [SerializeField] private int _IntelectWeapon;

    private void Awake()
    {
        StartWeaponInfo();
    }
    private void StartWeaponInfo()
    {
        _lvlWeapon = YG2.saves.WeaponsLevel[WeaponPriceInfo.id];
        _countCardsWeapon = YG2.saves.WeaponsCard[WeaponPriceInfo.id];
         for(int i = 0; i < _lvlWeapon; i++)
        {
            _countCardsWeaponMax *= 2;
        }

        _priceWeapon = WeaponPriceInfo.Price;
        _priceWeaponText.text = _priceWeapon.ToString();
        _imageWeapon.sprite = WeaponPriceInfo.IconWeapon;
        _countCardsText.text = _countCardsWeapon.ToString() + "/" + _countCardsWeaponMax.ToString();
        _lvlText.text = _lvlWeapon.ToString();
        if(_countCardsWeapon <= 0 && _lvlWeapon <= 0)
        {
            _imageWeapon.color = Color.black;
        }
/*        _StreghtWeapon = WeaponPriceInfo.StreghtWeapon;
        _AggilityWeapon = WeaponPriceInfo.AggilityWeapon;
        _IntelectWeapon = WeaponPriceInfo.IntelectWeapon;*/
    }

    private void UPLevelWeapon()
    {
        if(_countCardsWeapon >= _countCardsWeaponMax)
        {
            _lvlWeapon += 1;
            _countCardsWeapon -= _countCardsWeaponMax;
            _countCardsWeaponMax *= 2;

        }
            UpdateUI();
    }
    private void UpdateUI()
    {
        YG2.saves.WeaponsLevel[WeaponPriceInfo.id] = _lvlWeapon;
        YG2.saves.WeaponsCard[WeaponPriceInfo.id] = _countCardsWeapon;
        _lvlText.text = _lvlWeapon.ToString();
        _countCardsText.text = _countCardsWeapon.ToString() + "/" + _countCardsWeaponMax.ToString();
    }
    public void BuyAndSelectWeapon()
    {
        if(_priceWeapon <= CoinCounter.Instance.ReturnCoin() && _countCardsWeapon >= _countCardsWeaponMax)
        {
            CoinCounter.Instance.RemoveCoin(_priceWeapon);
            WeaponManager.Instance.SelectWeapon(WeaponPriceInfo.id);
            UPLevelWeapon();
            YG2.SaveProgress();
        }
        else if(_lvlWeapon > 0)
        {
            WeaponManager.Instance.RemoveStatsWeapon(YG2.saves.SelectWeaponID);
            WeaponManager.Instance.SelectWeapon(WeaponPriceInfo.id);
            YG2.SaveProgress();
            Debug.Log(" Selected");
        }
        StatsManager.Instance.EventAction();
        Debug.Log(YG2.saves.WeaponsLevel[WeaponPriceInfo.id]);
    }
    public void AddCard(int cardCount)
    {
        _countCardsWeapon += cardCount;
        YG2.saves.WeaponsCard[WeaponPriceInfo.id] = _countCardsWeapon;
        UpdateUI();
        YG2.SaveProgress();
    }
    public void DirrectionClick()
    {

            WeaponManager.Instance.LoadWeaponDirrection(WeaponPriceInfo);      
    }    
}
