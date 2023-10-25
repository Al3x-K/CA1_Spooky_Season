using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool toggleMusic, toggleEffects;

    public void Toggle()
    {
        if(toggleEffects) AudioManager.instance.ToggleEffects();
        if(toggleMusic) AudioManager.instance.ToggleMusic();
    }
}
