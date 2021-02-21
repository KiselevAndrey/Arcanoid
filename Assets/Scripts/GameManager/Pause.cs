using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioMixerGroup mixer;

    [Header("Snapshots")]
    [SerializeField] AudioMixerSnapshot normal;
    [SerializeField] AudioMixerSnapshot pause;

    bool _isPause = false;

    private void Start()
    {
        ChangeSnapshots();
    }

    public void Paused()
    {
        _isPause = !_isPause;
        pauseMenu.SetActive(_isPause);

        Time.timeScale = _isPause ? 0 : 1;

        ChangeSnapshots();
    }

    void ChangeSnapshots()
    {
        switch (_isPause)
        {
            case true:
                pause.TransitionTo(1f);
                break;

            case false:
                normal.TransitionTo(1f);
                break;
        }
    }

    public void ToggleMusic(bool enabled) => mixer.audioMixer.SetFloat(MixerGroup.VolumeBackGround, enabled ? 0 : -80);

    public void ChangeVolume(float volume) => mixer.audioMixer.SetFloat(MixerGroup.VolumeMaster, Mathf.Lerp(-50, 0, volume));
}
