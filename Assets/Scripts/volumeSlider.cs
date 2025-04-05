using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Set slider value to current volume on start
        volumeSlider.value = AudioListener.volume;

        // Add listener to handle value change
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
