using UnityEngine;

public enum TypeStats { STR, AGL, INT }
[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Create New Stats")]
public class StatsInfo : ScriptableObject
{
    public string NameStats;
    public int CountStats;
    public TypeStats TypeStats;
    public float ModiferUPStats;
}
