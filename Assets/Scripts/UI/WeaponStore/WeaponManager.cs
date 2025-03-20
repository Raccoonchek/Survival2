using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject _store;
    [SerializeField] private Transform _weaponPoin;
    [SerializeField] private Transform _derectionWeaponPoin;
    [SerializeField] private Transform _statsWeaponPoin;

    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private GameObject[] _statsWeaponDirection;
    [SerializeField] private GameObject[] _statsWeapon;
    [SerializeField] private int _currentWeaponID;

    [SerializeField] private Image _weaponImage;

    public static WeaponManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _weapons = new GameObject[_weaponPoin.childCount];
        _statsWeaponDirection = new GameObject[_derectionWeaponPoin.childCount];
        _statsWeapon = new GameObject[_statsWeaponPoin.childCount];
        if (_store.activeSelf == true)
        {
            _store.SetActive(false);
        }
        LoadWeapon();
    }
    private void LoadWeapon()
    {
        for (int i = 0; i < _statsWeaponPoin.childCount; i++)
        {
            _statsWeapon[i] = _statsWeaponPoin.GetChild(i).gameObject;
        }
        for (int i = 0; i < _derectionWeaponPoin.childCount; i++)
        {
            _statsWeaponDirection[i] = _derectionWeaponPoin.GetChild(i).gameObject;
        }
        for (int i = 0; i < _weaponPoin.childCount; i++)
        {
            if (_weaponPoin.GetChild(i).gameObject.activeSelf == false)
            {
                _weaponPoin.GetChild(i).gameObject.SetActive(true);
            }
            _weapons[i] = _weaponPoin.GetChild(i).gameObject;
            _weaponPoin.GetChild(i).gameObject.SetActive(false);
        }
        if(YG2.saves.WeaponSelected == true)
        {
            SelectWeapon(YG2.saves.SelectWeaponID);
        }
    }
    public void SelectWeapon(int id)
    {
        _currentWeaponID = id;
        _weapons[_currentWeaponID].SetActive(true);
        YG2.saves.SelectWeaponID = _currentWeaponID;
        if(YG2.saves.WeaponSelected == false)
        {
            YG2.saves.WeaponSelected = true;
        }
        if(StatsManager.Instance.isStatsPanelLoaded)
        {
             StatsManager.Instance.LoadStats();
        }
        YG2.SaveProgress();
    }

    public void RemoveStatsWeapon(int ID)
    {
        if (YG2.saves.WeaponSelected == true)
        { 
            _weapons[ID].SetActive(false);
            StatsManager.Instance.ResetStats(ID);
            Debug.Log("RemoveStatsl");

        }
    }

    public int AddStatsWeapon(int id)
    {
        if (YG2.saves.WeaponsLevel[YG2.saves.SelectWeaponID] == 0)
        {
            return 0;
        }
        return _statsWeapon[_currentWeaponID].GetComponent<WeaponPanel>().WeaponPriceInfo.StatsWeapon[id];
    }
    public void OpenStore(bool value)
    {
        _store.SetActive(value);
    }
    public void LoadWeaponDirrection(WeaponPriceInfo PriceInfo)
    {
        for (int i = 0; i < _statsWeaponDirection.Length; i++)
        {
            _statsWeaponDirection[i].transform.GetChild(0).GetComponent<TMP_Text>().text = " " + PriceInfo.StatsWeapon[i];
        }
        _weaponImage.sprite = PriceInfo.IconWeapon;
    }
    public void AddRandomCardsToWeapons(int CountAddCards, TMP_Text countAddCards, Image IconCard)
    {
        int RandomWeapon = Random.Range(0, _statsWeapon.Length);
        WeaponPanel weaponPanel = _statsWeapon[RandomWeapon].GetComponent<WeaponPanel>();
        weaponPanel.AddCard(CountAddCards);

        IconCard.sprite = weaponPanel.WeaponPriceInfo.IconWeapon;
        countAddCards.text = CountAddCards.ToString();

    }
}
