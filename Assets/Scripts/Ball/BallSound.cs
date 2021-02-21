using UnityEngine;

public enum BallStatus { hit, dead }
public class BallSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("Звуки")]
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip dead;

    public void PlayOneShot(BallStatus ballStatus)
    {
        AudioClip temp = hit;
        switch (ballStatus)
        {
            case BallStatus.hit:
                temp = hit;
                break;
            case BallStatus.dead:
                temp = dead;
                break;
        }

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(temp);
    }
}
