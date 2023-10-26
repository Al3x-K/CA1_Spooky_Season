using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    private AudioSource soundEffectSource; 
    private AudioSource backgroundMusicSource; 

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip footstepSound; 
    public AudioClip attackSound; 
    public AudioClip backgroundMusic; 
    public AudioClip clickSound;
   
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

        soundEffectSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    
    }
  
    
    public void PlayBackgroundMusic()
    {
        if(!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
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


    public void PlayJumpSound()
    {
        soundEffectSource.PlayOneShot(jumpSound);
    }
    public void PlayHitSound()
    {
        soundEffectSource.PlayOneShot(hitSound);
    }
    public void PlayFootstepSound()
    {
        soundEffectSource.PlayOneShot(footstepSound);
    }
    public void PlayAttackSound()
    {
        soundEffectSource.PlayOneShot(attackSound);
    }

    public void PlayClickSound()
    {
        soundEffectSource.PlayOneShot(clickSound);
    }
}