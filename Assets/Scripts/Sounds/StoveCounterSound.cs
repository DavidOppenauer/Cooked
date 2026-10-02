using System;
using System.Threading;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float warningSoundTimer;

    private bool playWarningSound = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        // Block 3 new sound for warning
        float burnShowProgressAmount = 0.5f;
        // Little inefficent but not as ugly
        playWarningSound = false;
        if (e.progressNormalized >= burnShowProgressAmount && stoveCounter.IsFried())// Show when the timer is smaller than 50%
        {
            playWarningSound = true; 
        }        
    }

    private void Update()
    {
        if(playWarningSound == true)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = 0.2f; // 5 times per second
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position); // play at the stoves position
            }
        }
        
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnOvenStateChangedEventArgs e)
    {
        //Block1 variable setting
        bool playSound = false;
        if (e._state == StoveCounter.State.Frying)
        {
            playSound = true;
        } else if(e._state == StoveCounter.State.Fried)
        {
            playSound = true;
        } else
        {
            playSound = false;
        }
        // Block2 play sound
        if (playSound == true)
        {
            audioSource.Play();
        } else
        {
            audioSource.Pause();
        }
    }
}