using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    
    private void Awake()
    {
        //playButton.onClick.AddListener(() => { 
        // Click Code
        // }); valid
        playButton.onClick.AddListener(PlayClick);
        quitButton.onClick.AddListener(() => { Application.Quit(); });// same thing

        // Reset the time multiplier in case it was paused
        Time.timeScale = 1f;
    }

    private void PlayClick()
    {
        //SceneManager.LoadScene(1);
        Loader.Load(Loader.Scene.MainScene); // Shows error if it doesnt match i guess??
    }
}
