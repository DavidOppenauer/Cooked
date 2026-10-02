using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private int previousCountdownNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCounDownToStartTimer());
        //countdownText.text = GameManager.Instance.GetCounDownToStartTimer().ToString(); // ToString is really nice and important!!!!!
        //countdownText.text = GameManager.Instance.GetCounDownToStartTimer().ToString("F1"); // Limit decimals F0 also no decimals but ends on 0
        countdownText.text = countDownNumber.ToString(); // No Decimals, ends with 1

        // We do this so we know when to start the next animation, which is the that the number changes.
        if(previousCountdownNumber != countDownNumber)
        {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
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
