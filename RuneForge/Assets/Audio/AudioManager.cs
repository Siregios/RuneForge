using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioSource musicSource;

    public AudioClip[] audioClips;

    //reset music between scenes?
    public bool resetOnSceneChange;

    //remove once audio update is complete
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += SceneReset;
	}
	
    public void PlaySound(int sound)
    {
        audioSource.PlayOneShot(audioClips[sound]);
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void PlaySFXClip(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    //Selects and plays a random clip from a given selection
    public void PlayRandomSFXClip(params AudioClip[] clips)
    {
        if (clips.Length == 0)
            Debug.LogError("No AudioClips inputted");
        int randomIndex = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[randomIndex]);
    }

    //Note: AudioManager is set to only play one song at a time
    //To multi-track separate implementation is required
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlayMusicScheduled(AudioClip clip, double time, bool loop = true)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.PlayScheduled(time);
    }

    public bool isMusicPlaying()
    {
        return musicSource.isPlaying;
    }

    public bool isSFXPlaying()
    {
        return sfxSource.isPlaying;
    }


    public void StopMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }

    //Warning: This will stop ALL currently playing SFX
    //But NOT those called by PlayOneShot
    //Since all SFX are called using PlayOneShot, this is mainly
    //for debugging purposes in case the sfxSource is working improperly
    public void StopSFX()
    {
        if (sfxSource.isPlaying)
            sfxSource.Stop();
    }

    //Sets the master music volume where volume is a float in the range (0, 1.0)
    public void SetMusicVolume(float volume)
    {
        if (volume < 0 || volume > 1)
        {
            Debug.LogError("Input volume out of range");
            //Try this later
            //Debug.LogErrorFormat
            return;
        }
        musicSource.volume = volume;
    }

    //Sets the master SFX volume where volume is a float in the range (0, 1.0)
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    private void SceneReset(Scene previousScene, Scene newScene)
    {
        //remove these if statments once update is complete
        if (resetOnSceneChange)
        {
            if (musicSource)
                StopMusic();
            if (sfxSource)
                StopSFX();
        }
    }
}
