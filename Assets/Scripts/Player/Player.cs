using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Связывающие данные")]
    public GameManager gameManager;
    [HideInInspector] public int indexInGame;

    [Header("Скрипты связанные с игроком")]
    public PlayerSO playerSO;
    public PlayerMove move;
    public PlayerScore score;
    public PlayerStats stats;
    public Savior savior;
    public PlayerMagnette magnette;


    public void Hit()
    {
        score.Hit();
        stats.Hit();
    }

    #region NewGame Load Save GameOver
    public void LoadPlayer(LVLStatsSO lvlStats)
    {
        stats.SetLife(lvlStats.startLifes);
        stats.SetDamage(lvlStats.startDamage);
        move.SetSpeed(lvlStats.startSpeed);
        savior.SetLife(lvlStats.startSaviorLifes);
    }

    public void LoadPlayer()
    {
        stats.SetLife(playerSO.startLifes);
        stats.SetDamage(playerSO.startDamage);
        move.SetSpeed(playerSO.startSpeed);
        savior.SetLife(playerSO.startSaviorLife);
    }

    public void GameOver()
    {
        score.GameOver();
    }
    #endregion

    
}
