using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockCounter : MonoBehaviour
{
    [Header("Связывающие данные")]
    public GameManager gameManager;

    [Header("Доп данные")]
    [SerializeField, Range(0, 100)] int bonusChance;
    
    BonusInstantiater _bonusInstantiater;
    int _blockCount;

    #region Awake Start
    private void Awake()
    {
        _bonusInstantiater = GetComponent<BonusInstantiater>();
    }

    void Start()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        _blockCount = blocks.Length;

        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].isImmortal) _blockCount--;
            //SetBlockLife(blocks[i]);
            SetBlockScore(blocks[i]);
        }
    }
    #endregion

    #region Set Block Life/Score
    //void SetBlockLife(Block block)
    //{
    //    if (block.GetLifes() == 0)
    //    {
    //        string sceneName = SceneManager.GetActiveScene().name;
    //        int lvl = int.Parse(sceneName[sceneName.Length - 1].ToString());
    //        block.SetLifes(lvl);
    //    }
    //}

    void SetBlockScore(Block block)
    {
        if (block.score == 0)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            int lvl = int.Parse(sceneName[sceneName.Length - 1].ToString());
            block.score = lvl * 2;
        }
    }
    #endregion

    #region BlockDied
    public void BlockDied(Vector2 position)
    {
        _blockCount--;
        CheckKillAll();

        if (Random.Range(0, 101) <= bonusChance)
            _bonusInstantiater.CreateBonus(position, bonusChance, 1);
    }

    void CheckKillAll()
    {
        if (_blockCount <= 0)
        {
            print("win");
        }
    }
    #endregion

    void Win()
    {
        gameManager.Win();
    }
}
