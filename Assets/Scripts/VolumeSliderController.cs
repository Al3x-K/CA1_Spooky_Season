using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.changeVolume(slider.value);
        slider.onValueChanged.AddListener(value => AudioManager.instance.changeVolume(value));
    }


}
