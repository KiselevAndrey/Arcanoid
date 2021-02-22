using UnityEngine;

[CreateAssetMenu(fileName ="Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Стартовые характеристики")]
    [Min(1)] public int startLifes;
    [Min(1)] public int startDamage;
    [Min(1)] public float startSpeed;
    [Min(1)] public int startSaviorLife;

    [Header("Счет")]
    public int score;
    public int currentScore;

    public void AddScore(int value)
    {
        score += value;
        currentScore += value;
    }

    public void NewGame()
    {
        score = 0;
        NewRound();
    }

    public void NewRound()
    {
        currentScore = 0;
    }

    public void SavePlayer(Player player)
    {

    }

    public void LoadPlayer(ref Player player)
    {

    }
}
