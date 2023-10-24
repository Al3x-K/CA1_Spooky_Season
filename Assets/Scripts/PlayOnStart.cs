using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound(clip);
    }

  
}
