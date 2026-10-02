using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    //private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                /*waitingToStartTimer -= Time.deltaTime; // 1 - real life time between frames? I guess it checks out
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }*/
            break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime; // 1 - real life time between frames? I guess it checks out
                if(countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
            break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime; // 1 - real life time between frames? I guess it checks out
                if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
            break;
            case State.GameOver:
            break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying; // quick one liner syntax for bools only prbly though
    }
    public bool IsGameOver()
    {
        return state == State.GameOver; // quick one liner syntax for bools only prbly though
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCounDownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        //return 1-(gamePlayingTimer / gamePlayingTimerMax);
        return gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            // timeScale is the multiplier all other deltaTimes run off, so putting this on 0 will basically halt or stall all calculation regarding time;
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } 
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}