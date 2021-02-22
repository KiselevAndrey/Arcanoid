using UnityEngine;

public enum GameStatus { New, Load }

[CreateAssetMenu(fileName = "GameOptionsSO")]
public class GameOptionsSO : ScriptableObject
{
    public int playersCount;
    [Min(1)] public int maxLevel;
    public GameStatus gameStatus;

    public void UpdateMaxLVL(int value) => maxLevel = Mathf.Max(maxLevel, value);
}
