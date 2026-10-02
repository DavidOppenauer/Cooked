using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;
    [SerializeField] private float footStepVolume = 1f;

    private void Awake()
    {
        player = GetComponent<Player>(); // Ah yeah because were on the player bruh
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.GetIsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, footStepVolume);
            }   
        }
    }
}