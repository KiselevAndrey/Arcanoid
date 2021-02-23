using UnityEngine;

public class PlayerMagnette : MonoBehaviour
{
    [SerializeField] ParticleSystem magnettePartical;

    bool _isMagnette;
    bool _haveMagnetteBall;

    public void SetMagnette(float value)
    {
        _isMagnette = value > 0;

        if (_isMagnette) magnettePartical.Play();
        else magnettePartical.Stop();
    }

    /// <summary>
    /// Можно ли примагнитить мяч
    /// </summary>
    public bool CanSetMagnetteBall()
    {
        if (!_isMagnette) return false;
        if (_haveMagnetteBall) return false;
        return true;
    }

    public void HaveMagnetteBall(bool value) => _haveMagnetteBall = value;
}
