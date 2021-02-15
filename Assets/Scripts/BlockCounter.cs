using UnityEngine;

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
    }
    #endregion

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
}
