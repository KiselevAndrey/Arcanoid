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

    bool _isMagnette = true;
    bool _haveMagnetteBall;
    
    public void Hit()
    {
        score.Hit();
        stats.Hit();
    }

    public void GameOver()
    {
        score.GameOver();
    }

    #region Magnette и все все все
    public void SetMagnette(float value) => _isMagnette = value > 0;

    /// <summary>
    /// Можно ли примагнитить мяч
    /// </summary>
    public bool CanSetMagnetteBall()
    {
        if (!_isMagnette) return false;
        if (_haveMagnetteBall) return false;
        return true;
    }

    public void HaveMagnetteBall(bool value) => _haveMagnetteBall = value;
    #endregion
}
