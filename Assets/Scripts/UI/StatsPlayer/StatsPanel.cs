using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StatsPanel : MonoBehaviour
{
    [HideInInspector]
    public StatsManager StatsManager;

    public StatsInfo StatsInfo;

    [SerializeField] private int _countStats;

    [SerializeField] private GameObject _buttonUp;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _countText;
    private void Awake()
    {
        StatsManager.OnUpStatsEvent.AddListener(UpdateUI);
    }
    public void UpStats(int count)
    {
        if(StatsManager._pointUPStats > 0)
        {
            _countStats += count;
            StatsManager._pointUPStats -= 1;
            StatsManager.SaveStats();
            if (StatsManager._pointUPStats <= 0)
            {
                StatsManager.OpenAndCloseButtonUp(false);
            }

            StatsManager.EventAction();
        }

    }
    public void UpdateUI()
    {
        _countText.text = _countStats.ToString();
        Debug.Log("UPDATE UI COMLIDET");
    }
    public void LoadStats(int value)
    {
        _countStats = value;
        _nameText.text = StatsInfo.NameStats;
        _countStats += StatsInfo.CountStats;

        if(StatsManager._pointUPStats <= 0)
        {
            CloseButtonUp(false);
        }
        StatsManager.EventAction();
    }
    public int ReturnStats()
    {
        int value = _countStats - StatsInfo.CountStats;
        return value;
    }
    public void CloseButtonUp(bool value)
    {
        if(_buttonUp != null)
        _buttonUp.SetActive(value);
    }

}
