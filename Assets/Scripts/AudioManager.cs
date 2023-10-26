using UnityEngine;

//Base of the script was taken from Naoise's script that was coded during the class
//CHANGES MADE:
//1. I adjusted the variables that were created to my needs (deleted dashSound)
//2. I added a function called Toggle, that allows me to mute or unmute music or 
//sound effects by clicking on the buttons in the settings or pause menu
//3. Also adjusted the ChangeVolume function
//4. I used my own sounds that were either downloaded from itch.io, or created by using rfxgen 
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //this allows me to use functions from this script in others
    
    //Music and Sound Effects
    private AudioSource soundEffectSource; 
    private AudioSource backgroundMusicSource; 

    //variables for Audio clips
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

        //adding AudioSource to the AudioManager object
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.clip = backgroundMusic; //asigning a clip
        backgroundMusicSource.loop = true; //lopping the source
        backgroundMusicSource.Play();
    
    }
  
    //Plays Music all the time
    public void PlayBackgroundMusic()
    {
        if(!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }
    public void changeVolume(float value)
    {
        AudioListener.volume = value; //it sets volume by using a slider (more in slider script)
    }


    //Muting/unmuting sound effects
    public void ToggleEffects()
    {
        soundEffectSource.mute = !soundEffectSource.mute;
    }

    //Muting/unmuting music 
    public void ToggleMusic()
    {
        backgroundMusicSource.mute = !backgroundMusicSource.mute;
    }

    //Various sounds effect functions
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

    //the sound of the click of the button in the menu
    public void PlayClickSound()
    {
        soundEffectSource.PlayOneShot(clickSound);
    }
}