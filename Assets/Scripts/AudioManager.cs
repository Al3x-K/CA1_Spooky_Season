using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    [SerializeField] private AudioSource soundEffectSource; 
    [SerializeField] private AudioSource backgroundMusicSource; 
   
    public AudioClip [] soundsEffects;
    void Awake()
    {
        //checks if the instance of Audio Manager already exists and if yes, destroys 
        //the one created right now
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; 
        }
    
    }
  
    public void PlaySound(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }

    public void changeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        soundEffectSource.mute = !soundEffectSource.mute;
    }

    public void ToggleMusic()
    {
        backgroundMusicSource.mute = !backgroundMusicSource.mute;
    }
    
}