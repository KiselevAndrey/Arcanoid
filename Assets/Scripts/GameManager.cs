using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winLog;
    
    [HideInInspector] public List<Player> players;
    [HideInInspector] public List<Ball> balls;

    #region Awake Start
    private void Awake()
    {
        players = FindObjectsOfType<Player>().ToList();
        balls = FindObjectsOfType<Ball>().ToList();

        SetIndex();
    }

    private void Start()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].move.Zeroing();
        }
    }
    #endregion

    /// <summary>
    /// Установка индексов у объектов
    /// </summary>
    void SetIndex()
    {
        for (int i = 0; i < players.Count; i++)
            players[i].indexInGame = i;

        for (int i = 0; i < balls.Count; i++)
            balls[i].indexInGame = i;
    }

    public void Win()
    {
        winLog.SetActive(true);
        for (int i = 0; i < balls.Count; i++)
        {
            if (i < players.Count) balls[i].move.Zeroing();
            else Destroy(balls[i].gameObject);
        }
    }

}
