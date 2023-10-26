using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows me to mute or unmute music and sounds effects by using buttions in settings or pause menu 
public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool toggleMusic, toggleEffects;

    public void Toggle()
    {
        //uses toggle functions from audio manager script which are called onClick on the buttons
        if(toggleEffects) AudioManager.instance.ToggleEffects(); 
        if(toggleMusic) AudioManager.instance.ToggleMusic();
    }
}
