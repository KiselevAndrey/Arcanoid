using UnityEngine;

[CreateAssetMenu(fileName = "Bonus")]
public class BonusSO : ScriptableObject
{
    public Sprite sprite;
    public BonusName bonusName;
    [Min(0)] public float multiply;

    [HideInInspector] public bool isPositive;

    public float Force { get; private set; }
    public void SetForce(float value) => Force = value * (isPositive ? 1 : -1);
}
