using UnityEngine;

public enum GameStatus { New, Load }

[CreateAssetMenu(fileName = "GameOptionsSO")]
public class GameOptionsSO : ScriptableObject
{
    public int playersCount;
    public int maxLevel;
    public GameStatus gameStatus;
}
