using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    [Header("Цвета")]
    [SerializeField] Color normalColor;
    [SerializeField] Color overrunColor;
    
    [Header("Цифры")]
    [SerializeField] NumberSO numbers;

    List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>();
    int _visiblePeriodCount;
    bool _isOverrun;

    private void Awake()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
            _spriteRenderers.Add(spriteRenderers[i]);

        if (_isOverrun) UpdateColor(overrunColor);
        else UpdateColor(normalColor);
    }

    public void SetNumber(int value)
    {
        if (value >= Mathf.Pow(10, _spriteRenderers.Count))
        {
            for (int i = 0; i < _spriteRenderers.Count; i++)
                UpdateNumber(i, 9);

            if (!_isOverrun)
            {
                UpdateColor(overrunColor);
                _isOverrun = true;
            }
        }
        else
        {
            //bool check = CheckVisiblePeriodCount(value);

            for (int i = 0; i < _spriteRenderers.Count; i++)
            {
                if (value>0)
                {
                    UpdateNumber(i, value % 10);
                    value /= 10;
                }
                else
                    _spriteRenderers[i].gameObject.SetActive(false);
            }

            if (_isOverrun)
            {
                UpdateColor(normalColor);
                _isOverrun = false;
            }
        }
    }

    void UpdateNumber(int period, int value)
    {
        _spriteRenderers[period].gameObject.SetActive(true);
        _spriteRenderers[period].sprite = numbers.GetNumber(value);
    }

    void UpdateColor(Color color)
    {
        for (int i = 0; i < _spriteRenderers.Count; i++)
            _spriteRenderers[i].color = color;
    }

    void UpdateSpritePositions()
    {

    }

    bool CheckVisiblePeriodCount(int number)
    {
        int i = 0;
        while (number > 0)
        {
            number /= 10;
            i++;
        }
        bool tmp = i == _visiblePeriodCount;

        _visiblePeriodCount = i;

        return tmp;
    }
}
