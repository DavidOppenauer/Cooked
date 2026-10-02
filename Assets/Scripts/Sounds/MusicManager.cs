using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance{ get; private set; }
    private float musicVolume;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        // Because it plays right away we need to change it immeadiatly
        audioSource.volume = musicVolume;
    }
    // Function to modify the general volume
    public void ChangeVolume()
    {
        musicVolume += .1f;
        if(musicVolume > 1f)
        {
            musicVolume = 0f;
        }
        audioSource.volume = musicVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return musicVolume;
    }
}
