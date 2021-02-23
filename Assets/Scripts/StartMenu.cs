using UnityEngine;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mixer;

    [Header("Snapshots")]
    [SerializeField] AudioMixerSnapshot normal;


    void Start()
    {
        normal.TransitionTo(1f);
    }
}
