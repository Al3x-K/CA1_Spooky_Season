using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

   
    public AudioClip backgroundMusic;

    private AudioSource soundEffectSource; 
    private AudioSource backgroundMusicSource; 


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
    
    //it adds component of type AudioSource to the game object
    soundEffectSource = gameObject.AddComponent<AudioSource>();
    backgroundMusicSource = gameObject.AddComponent<AudioSource>();

    backgroundMusicSource.clip = backgroundMusic;//conects the clip to the source
    backgroundMusicSource.loop = true; //loops the music
    backgroundMusicSource.Play();//plays the sound from the source
    }
  
    //Background music section
    public void PlayBackgroundMusic()
    {
        //checks if the source is already playing and if not, activate it
        if(!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }
    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }

    //Sound effects section
    public void PlaySoundEffects()
    {
        soundEffectSource.Play();
    }
    public void PauseSoundEffects()
    {
        soundEffectSource.Pause();
    }
    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectSource.volume = volume;
    }
}