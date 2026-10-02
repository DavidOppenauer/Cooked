using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectsSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    [SerializeField] private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    [SerializeField] private int platesSpawnedAmountMax = 10;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount <= platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, new EventArgs());
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is empty handed
            if (platesSpawnedAmount > 0)
            {
                // There is at least one plate
                platesSpawnedAmount--;
                OnPlateRemoved?.Invoke(this, new EventArgs());
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            }
        }
    }
}
