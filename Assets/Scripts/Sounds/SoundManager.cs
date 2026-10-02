using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float soundVolume;

    private void Awake()
    {
        Instance = this;

        soundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f); // The first time it runs it uses this default 1f
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlayedOnAnyCounter += BaseCounter_OnAnyObjectPlayedOnAnyCounter;
        TrashCounter.OnObjectTrashedAny += TrashCounter_OnObjectTrashedAny;
    }

    private void TrashCounter_OnObjectTrashedAny(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter; // could also cast as basecounter
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlayedOnAnyCounter(object sender, EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender; // both valid casts
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        //Debug.Log(transform.position); Was for testing, because static events that wanted to call this but it didnt exist anymore
        CuttingCounter cuttingCounter = sender as CuttingCounter; //Holy crap this works too???--------------------------------------------------
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) // YOU CAN PUT DEFAULT VALUES IN HERE DAMN
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * soundVolume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
    public void PlayCountDownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }



    // Function to modify the general volume
    public void ChangeVolume()
    {
        soundVolume += .1f;
        if(soundVolume > 1f)
        {
            soundVolume = 0f;
        }

        // How to safe settings in Unity: 1. Shown here changing the PlayerPrefs. <- Its essentially a dictionary
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, soundVolume);
        PlayerPrefs.Save(); // Always set and save to be save
    }
    public float GetVolume()
    {
        return soundVolume;
    }
}