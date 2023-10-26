using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script allows me to control the volume of music and sound effect by using sliders in settings menu and pause menu
public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    void Start()
    {
        AudioManager.instance.changeVolume(slider.value); //calls a function from audio manager 
        slider.onValueChanged.AddListener(value => AudioManager.instance.changeVolume(value)); //Callback executed when the value of the slider is changed 
    }


}
