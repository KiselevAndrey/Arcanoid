using System.Collections.Generic;
using UnityEngine;

public enum BonusName { Random, Damage, SpeedPlatform, Score, DublicateBall }

public class BonusInstantiater : MonoBehaviour
{
    public List<BonusSO> bonuses;
    public GameObject bonusPrefab;

    [SerializeField, Range(0, 100)] int multyplyGoodBonus;


    public void CreateBonus(Vector3 position, int seed, int difficult)
    {
        Bonus bonus = Instantiate(bonusPrefab).GetComponent<Bonus>();
        bonus.transform.position = position;
        bonus.NewBonus(bonuses.Ind(Random.Range(0, seed)));
        bonus.IsPositive(Helper.RandomBoolPercent(multyplyGoodBonus));
        bonus.SetForce(difficult);
    }
}
