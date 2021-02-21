using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LVLStatsSO")]
public class LVLStatsSO : ScriptableObject
{
    [Header("Стартовые характеристики")]
    [Min(1)] public int startLifes;
    [Min(1)] public int startDamage;
    [Min(1)] public float startSpeed;
    [Min(0)] public int saviorLifes;

    [HideInInspector] public int bestScore;

    public void UpdateBestScore(int currentScore) => bestScore = Mathf.Max(currentScore, bestScore);
}
