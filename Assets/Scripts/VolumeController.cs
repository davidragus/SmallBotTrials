using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = SoundManager.MasterVolume;
    }

    void SetVolume(float value)
    {
        SoundManager.MasterVolume = value;
    }
}

