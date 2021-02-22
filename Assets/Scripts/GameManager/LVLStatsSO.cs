using UnityEngine;

[CreateAssetMenu(fileName = "LVLStatsSO")]
public class LVLStatsSO : ScriptableObject
{
    [Header("Стартовые характеристики игрока")]
    [Min(1)] public int startLifes;
    [Min(1)] public int startDamage;
    [Min(1)] public float startSpeed;
    [Min(0)] public int startSaviorLifes;

    [Header("Характеристики уровня")]
    public int bestScore;
}
