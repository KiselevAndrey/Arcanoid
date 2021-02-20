using UnityEngine;

[CreateAssetMenu(fileName = "Bonus")]
public class BonusSO : ScriptableObject
{
    public Sprite sprite;
    public BonusName bonusName;
    [Min(0)] public float multiply;
}
