using UnityEngine;

public class LVLOptions : MonoBehaviour
{
    public LVLStatsSO lvlStats;

    public void UpdateBestScore(int currentScore) => lvlStats.bestScore = Mathf.Max(currentScore, lvlStats.bestScore);
}
