using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DontDestroyBGMusic : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider slider;

    private void Update()
    {
        GetVolume();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject); 
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    public void GetVolume()
    {
        audioMixer.GetFloat("volume", out float MV);
        slider.value = MV;
    }
}