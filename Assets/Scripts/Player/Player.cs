﻿using UnityEngine;

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
    
    public void Hit()
    {
        score.Hit();
        stats.Hit();
    }

    public void GameOver()
    {
        score.GameOver();
    }
}
