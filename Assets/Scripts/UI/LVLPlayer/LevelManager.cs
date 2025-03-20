using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelManager : MonoBehaviour
{
    private int Lvl = 0;
    private int _currentXP = 0;
    private float _maxXP = 100;
    private int _saveXP;

    [SerializeField] private float _sliderSpeed = 2;
    [SerializeField] private Slider _sliderLvl;
    [SerializeField] private Slider _sliderLvlBackground;
    [SerializeField] private TMP_Text _lvlText;

    public static LevelManager Instance;


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
    }

    private void Update()
    {
        UpdateSlideXP();
    }

    public void AddXP(int XP)
    {
        _currentXP += XP;
        _sliderLvlBackground.value = _currentXP;
        SaveXP();
        SaveLevel();
    }
    private void UPLVL()
    {
        if (_currentXP >= _maxXP && _sliderLvl.value >= _maxXP)
        {
            Lvl += 1;
            _currentXP = 0;
            _maxXP *= 1.25f;
            _maxXP = Mathf.Ceil(_maxXP);
            _sliderLvl.value = 0;
            _sliderLvl.maxValue = _maxXP;
            _sliderLvlBackground.maxValue = _maxXP;
            _sliderLvlBackground.value = _currentXP;

            _lvlText.text = Lvl.ToString();

            StatsManager.Instance.AddPoint();
            AddSaveXP();
            SaveLevel();
        }
    }
    private void SaveXP()
    {
        if(_currentXP > _saveXP)
        {
            _saveXP = _currentXP - Mathf.CeilToInt(_maxXP);
        }
    }

    private void AddSaveXP()
    {
        if(_saveXP <= 0)
        {
            return;
        }
        if(_saveXP <= _maxXP)
        {
            _currentXP += _saveXP;
            _saveXP = 0;
        }
        else
        {
            _currentXP += Mathf.CeilToInt(_maxXP);
            _saveXP -= Mathf.CeilToInt(_maxXP);
        }
        UpdateSliderLoadGame();
        SaveLevel();
    }
    private void UpdateSlideXP()
    {
        if (_sliderLvl.value < _sliderLvlBackground.value)
        {
            _sliderLvl.value += _sliderSpeed * Time.deltaTime;
            UPLVL();
        }
    }

    private void UpdateSliderLoadGame()
    {
        _lvlText.text = Lvl.ToString();

        _sliderLvl.maxValue = _maxXP;
        _sliderLvlBackground.maxValue = _maxXP;

        _sliderLvlBackground.value = _currentXP;
        UPLVL();

    }
    public void LoadLevel()
    {
        Lvl = YG2.saves.Lvl;
        _currentXP = YG2.saves.CurrentXP;
        _maxXP = YG2.saves.MaxXP;
        _sliderLvl.value = _currentXP;
        UpdateSliderLoadGame();
        _sliderSpeed = _maxXP / 2;
    }
    public void SaveLevel()
    {
        YG2.saves.Lvl = Lvl;
        YG2.saves.CurrentXP = _currentXP;
        YG2.saves.MaxXP = Mathf.CeilToInt(_maxXP);

        YG2.SaveProgress();
    }


    public void ResetSave()
    {
        YG2.SetDefaultSaves();
        YG2.SaveProgress();
    }
}
