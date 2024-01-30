using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public float volume;
    public float fovValue;
    public float sensitivityValue;

    private GameObject volumeSlider;
    private GameObject fovSlider;
    private GameObject sensitivitySlider;


    void Awake()
    {
        volume = PlayerPrefs.GetFloat("Volume", 0);     // Get the volume stored in PlayerPrefs, default to 0
        volumeSlider = GameObject.Find("VolumeSlider"); // Get the GameObject
        volumeSlider.GetComponent<Slider>().value = volume; // Get the Slider component, set its value to the volume(UI only)

        fovValue = PlayerPrefs.GetFloat("fovValue", 85);
        fovSlider = GameObject.Find("FOVSlider");
        fovSlider.GetComponent<Slider>().value = fovValue;

        sensitivityValue = PlayerPrefs.GetFloat("sensitivityValue", 7);
        sensitivitySlider = GameObject.Find("SensitivitySlider");
        sensitivitySlider.GetComponent<Slider>().value = sensitivityValue;
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Adjusting volume to:" + (volume));
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);         //Set a float value of volume to a key called "Volume" in PlayerPrefs
    }

    public void SetFOV(float fovValue)
    {
        Camera.main.fieldOfView =  fovValue;
        PlayerPrefs.SetFloat("fovValue", fovValue);
    }

    public void SetAimSensitivity(float sensitivityValue)
    {
        FindObjectOfType<SamuraiLook>().mouseSensitivity =  sensitivityValue;
        PlayerPrefs.SetFloat("sensitivityValue", sensitivityValue);
    }
}
