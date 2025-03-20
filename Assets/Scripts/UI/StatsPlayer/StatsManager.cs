using UnityEngine;
using UnityEngine.Events;
using YG;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Transform _StatsPerent;
    private StatsPanel[] _statsPanels = new StatsPanel[10];
    public int _pointUPStats = 0;

    public static StatsManager Instance;
    public static readonly UnityEvent OnUpStatsEvent = new UnityEvent();
    public bool isStatsPanelLoaded;


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
    private void Start()
    {

        _statsPanels = new StatsPanel[_StatsPerent.childCount];
        for (int i = 0; i < _StatsPerent.childCount; i++)
        {
            _statsPanels[i] = _StatsPerent.GetChild(i).GetComponent<StatsPanel>();
            _statsPanels[i].StatsManager = this;
        }
        isStatsPanelLoaded = true;
        LevelManager.Instance.LoadLevel();
        LoadStats();
    }

    public void EventAction()
    {
        OnUpStatsEvent.Invoke();
    }
    public void AddPoint()
    {
        _pointUPStats += 3;
        SaveStats();
        OpenAndCloseButtonUp(true);
    }

    public void OpenAndCloseButtonUp(bool value)
    {
        for (int i = 0; i < _statsPanels.Length; i++)
        {
            _statsPanels[i].CloseButtonUp(value);
        }
    }

    public void SaveStats()
    {
        YG2.saves.Point = _pointUPStats;
        for (int i = 0; i < _StatsPerent.childCount; i++)
        {
            YG2.saves.Stats[i] = _statsPanels[i].ReturnStats() - WeaponManager.Instance.AddStatsWeapon(i);
        }
        YG2.SaveProgress();
    }

    public void LoadStats()
    {
        _pointUPStats = YG2.saves.Point;
        for (int i = 0; i < _statsPanels.Length; i++)
        {
            _statsPanels[i].LoadStats(YG2.saves.Stats[i] + WeaponManager.Instance.AddStatsWeapon(i));
        }
        EventAction();
    }

    public void ResetStats(int ID)
    {
        if (ID != YG2.saves.SelectWeaponID)
        {
            for (int i = 0; i < _StatsPerent.childCount; i++)
            {
                int value = _statsPanels[i].ReturnStats() - WeaponManager.Instance.AddStatsWeapon(i);
                Debug.Log("Reset Stats = " + value.ToString());
                _statsPanels[i].LoadStats(_statsPanels[i].ReturnStats() - WeaponManager.Instance.AddStatsWeapon(i));
            }
        }
    }
    private StatsPanel ReturnStatsPanel(TypeStats typeStats)
    {
        for (int i = 0; i < _statsPanels.Length; i++)
        {
            if (_statsPanels[i].StatsInfo.TypeStats == typeStats)
            {
                return _statsPanels[i];
            }
        }
        return null;
    }
    public float ReturnUpStats(float startValue, TypeStats typeStats)
    {
        StatsPanel statsPanel = ReturnStatsPanel(typeStats);

        if (statsPanel != null)
        {
            float returnValue = (startValue + statsPanel.ReturnStats() + statsPanel.StatsInfo.CountStats) * statsPanel.StatsInfo.ModiferUPStats;
            return returnValue;
        }
        return startValue;
    }
}
