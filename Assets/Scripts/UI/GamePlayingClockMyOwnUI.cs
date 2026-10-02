using UnityEngine;
using System;
using TMPro;
public class GamePlayingClockMyOwnUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        //countdownText.text = GameManager.Instance.GetCounDownToStartTimer().ToString(); // ToString is really nice and important!!!!!
        clockText.text = GameManager.Instance.GetGamePlayingTimer().ToString("F1"); // Limit decimals F0 also no decimals but ends on 0
        //clockText.text = Mathf.Ceil(GameManager.Instance.GetCounDownToStartTimer()).ToString(); // No Decimals, ends with 1
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
