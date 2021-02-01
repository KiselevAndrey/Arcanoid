using System.Collections.Generic;
using UnityEngine;

public class BlockInstantiater : MonoBehaviour
{
    [Header("Данные по размеру")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [Header("Данные по стартовому уровню сложности")]
    [SerializeField, Min(1)] int blockCount;
    [SerializeField, Min(1)] int lineCount;
    [SerializeField, Min(0)] int difficultLvl;
    [SerializeField, Range(1, 100)] int complexityFactor;
    [SerializeField] bool randomInLine;

    [Header("Данные по блокам")]
    [SerializeField] List<GameObject> blocksType;
    [SerializeField, Min(0)] float blockSizeInWorldMatrix;

    List<int> _countBlocksInLines;
    int _iRow;
    int _deathCount;
    float _x, _y;
    int _maxRows, _maxColumns;

    Ball _ball;

    // Start is called before the first frame update
    void Start()
    {
        _maxColumns = (int)((maxX - minX) / blockSizeInWorldMatrix);
        _maxRows = (int)((maxY - minY) / blockSizeInWorldMatrix);

        _ball = GameObject.FindGameObjectWithTag(BDNames.Ball).GetComponent<Ball>();

        NewRound();
    }
    
    void NewRound()
    {
        CheckDifficult();
        CalculateLines();

        _deathCount = 0;

        _y = maxY;
        for (int i = 0; i < lineCount; i++)
        {
            bool randomize = false;
            GameObject block = blocksType[0];
            _x = (maxX + minX - _countBlocksInLines[i] / 2f) / 2;

            if (randomInLine && (i % 2 == 1) && (lineCount % 2 == 1)) randomize = true;
            else block = blocksType.Ind(Random.Range(0, blockCount));

            for (int j = 0; j < _countBlocksInLines[i]; j++)
            {
                if (randomize) block = blocksType.Ind(Random.Range(0, blockCount));

                Vector3 blockPos = new Vector2(_x, _y);
                Quaternion blockQ = new Quaternion();
                Block blockB = Instantiate(block, blockPos, blockQ).GetComponent<Block>();
                FillingBlock(ref blockB);

                _x += blockSizeInWorldMatrix;
            }
            _y -= blockSizeInWorldMatrix;
        }

        _ball.Zeroing();
    }

    // Высчитывает сколько блоков будет в строке
    void CalculateLines()
    {
        _countBlocksInLines = new List<int>();
        int rest = blockCount;

        for (int i = 0; i < lineCount - 1; i++)
        {
            int res = blockCount / lineCount;
            res = (int)Random.Range(res * 0.8f, res * 1.2f);

            if (res <= rest)
            {
                _countBlocksInLines.Add(res);
                rest -= res;
            }
            else
            {
                _countBlocksInLines.Add(rest);
                rest = 0;
            } 
        }

        _countBlocksInLines.Add(rest);
    }

    /// проверка что блоки не выходят за границу
    void CheckDifficult()
    {
        if (lineCount > _maxRows)
        {
            lineCount = _maxRows;
            UpdateComplexityFactor(1);
            UpdateDifficult();
        }
        if (blockCount > _maxRows * _maxColumns)
        {
            blockCount = _maxRows * _maxColumns;
            UpdateComplexityFactor(1);
            UpdateDifficult();
        }

        _ball.ChangeVelocity(difficultLvl);
    }

    // обновление сложности
    void UpdateDifficult()
    {
        if(complexityFactor >= Random.Range(0, 101))
        {
            difficultLvl++;
            if (difficultLvl > 9) difficultLvl = 9;
            UpdateComplexityFactor(-1);
        }
    }

    // обновление complexityFactor чтобы не заходил за границы 0 и 100
    void UpdateComplexityFactor(int addedValue)
    {
        complexityFactor += addedValue;
        if (complexityFactor < 0) complexityFactor = 0;
        else if (complexityFactor > 100) complexityFactor = 100;
    }

    void FillingBlock(ref Block block)
    {
        block.lifes = difficultLvl;
        block.score = Random.Range(difficultLvl - 1, difficultLvl + 2);
    }

    public void Hit()
    {
        _deathCount++;
        CheckKillAll();
    }

    void CheckKillAll()
    {
        if (_deathCount >= blockCount)
        {
            blockCount += Random.Range(5, 10);
            if (Random.Range(0, 2) > 0) lineCount++;

            NewRound();
        }
    }
}
