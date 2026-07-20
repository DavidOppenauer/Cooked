using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObejct;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnOvenStateChangedEventArgs e)
    {
        bool showVisual = false;
        if (e._state == StoveCounter.State.Frying || e._state == StoveCounter.State.Fried) { 
            showVisual = true; 
        }
        stoveOnGameObejct.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
