using UnityEngine;

public class BallStats : MonoBehaviour
{
    int _damage;

    public int Damage
    {
        get => _damage;
        set
        {
            _damage = value;
            if (_damage < 1) _damage = 1;
        }
    }
}
