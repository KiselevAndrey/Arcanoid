using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberSO")]
public class NumberSO : ScriptableObject
{
    [Header("Цифры")]
    [SerializeField] List<Sprite> numbers = new List<Sprite>();

    public Sprite GetNumber(int value) => numbers[value];
}
