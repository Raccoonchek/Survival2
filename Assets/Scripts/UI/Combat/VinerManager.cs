using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VinerManager : MonoBehaviour
{
    [SerializeField] private GameObject _openPanel;
    private string _nameScene;

    [SerializeField] private int _maxOpenChest;

    [SerializeField] private int _minCard;
    [SerializeField] private int _maxCard;

    [SerializeField] private Image _iconCard;
    [SerializeField] private TMP_Text _countCard;
    [SerializeField] private TMP_Text _countChest;

    public static VinerManager Instance;

    private int _countOpenChest;

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
        _openPanel.SetActive(false);
    }
    public  void GenerateCard(string nameScene)
    {
        _openPanel.SetActive(true);
        _countOpenChest = Random.Range(1, _maxOpenChest); // это сделать 1 раз в конце боя когда игрок зашел в портал  
        _countChest.text = _countOpenChest.ToString();

        _nameScene = nameScene;

    }

    public void OpenChest()
    {
        if (_countOpenChest > 0)
        {
            _countOpenChest -= 1;
            int RandCard = Random.Range(_minCard, _maxCard);
            WeaponManager.Instance.AddRandomCardsToWeapons(RandCard, _countCard, _iconCard);
            _countChest.text = _countOpenChest.ToString();
        }
        else
        {
            SceneManager.LoadScene(_nameScene);
        }    
    }


    


}
