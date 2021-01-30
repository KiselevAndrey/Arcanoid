using UnityEngine;

[CreateAssetMenu(fileName ="Player")]
public class PlayerSO : ScriptableObject
{
    [Min(1)] public int startLifes;

    [Header("Счет")]
    [Range(0,0)] public int score;
    [Range(0, 0)] public int bestScore;
    [Range(0, 0)] public int currentScore;

    public void Zeroing()
    {
        score = 0;
        bestScore = 0;
    }

    public void UpdateBestScore()
    {
        if (currentScore > bestScore) bestScore = currentScore;
        currentScore = 0;
    }

    public void AddScore(int value)
    {
        score += value;
        currentScore += value;
    }
}
