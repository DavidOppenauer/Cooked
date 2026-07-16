using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualArray;
    private void Start() // External references should always be in start while setting the reffered varaibles should be in awake
    {
        // THIS NEEDS TO RUN AFTER THE PLAYER INSTANCE WAS SET IN AWAKE OTHERWISE IT COULD BE NULL HERE
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounterArgs == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    // Kind of overKill bro
    private void Show()
    {
        foreach( GameObject selectedVisual in selectedVisualArray)
        {
            selectedVisual.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach( GameObject selectedVisual in selectedVisualArray)
        {
            selectedVisual.SetActive(false);
        }
    }
}
