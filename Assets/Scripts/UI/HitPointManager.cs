using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.HighDefinition.CameraSettings;

public class HitPointManager : MonoBehaviour
{
    [SerializeField] private Slider _sliderBackground;
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderDamage;
    [SerializeField] private float _speedHilling = 5f;
    [SerializeField] private PlayerHP _playerHP;

    public static HitPointManager Instance;

    private void Awake()
    {
        if(Instance == null)
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
        UpdateHp();
        HillingPlayer();
    }
    public void StartHitPointSlider(float value, float maxValue)
    {
        _sliderBackground.maxValue = maxValue;
        _sliderHP.maxValue = maxValue;
        _sliderBackground.value = value;
        _sliderDamage.maxValue = maxValue;
        _sliderDamage.value = maxValue;
        _sliderHP.value = maxValue;
    }
    public void DamagePlayerUI(float valueHP,float damage)
    {
        _sliderBackground.value = valueHP;
        _sliderHP.value -= damage;
    }
    public void UpdateSliderOfHilling(float valueHp)
    {
        _sliderBackground.value = valueHp;
    }
    private void UpdateHp()
    {
        if (_sliderHP.value <= _sliderBackground.value)
        {
            _sliderHP.value += _speedHilling * Time.deltaTime;
            if(_sliderDamage.value < _sliderHP.value)
            {
            _sliderDamage.value += _speedHilling * Time.deltaTime;
            }
        }
        if(_sliderDamage.value > _sliderHP.value)
        {
            _sliderDamage.value -= _speedHilling * Time.deltaTime;
        }
    }
    private void HillingPlayer()
    {
        _sliderBackground.value += _speedHilling * Time.deltaTime;

        _playerHP.Hilling(_speedHilling * Time.deltaTime);

    }
}
