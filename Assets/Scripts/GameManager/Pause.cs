using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioMixerGroup mixer;

    [Header("UI elements")]
    [SerializeField] Toggle musicSwitcher;
    [SerializeField] Slider volumSlider;

    [Header("Snapshots")]
    [SerializeField] AudioMixerSnapshot normal;
    [SerializeField] AudioMixerSnapshot pause;

    bool _isPause = false;

    private void Start()
    {
        LoadMusicOptions();
        ChangeSnapshots();
    }

    void LoadMusicOptions()
    {
        bool musicSwitch = PlayerPrefs.GetInt(MixerGroup.VolumeBackGround, 1) == 1;
        musicSwitcher.isOn = musicSwitch;
        ToggleMusic(musicSwitch);

        float volume = PlayerPrefs.GetFloat(MixerGroup.VolumeMaster, 1);
        volumSlider.value = volume;
        ChangeVolume(volume);
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

    public void ToggleMusic(bool enabled)
    {
        mixer.audioMixer.SetFloat(MixerGroup.VolumeBackGround, enabled ? 0 : -80);

        PlayerPrefs.SetInt(MixerGroup.VolumeBackGround, enabled ? 1 : 0);
    }

    public void ChangeVolume(float volume)
    {
        mixer.audioMixer.SetFloat(MixerGroup.VolumeMaster, Mathf.Lerp(-50, 0, volume));

        PlayerPrefs.SetFloat(MixerGroup.VolumeMaster, volume);
    }
}
