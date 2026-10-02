using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashedAny;
    new public static void ResetStaticData() // Here we have to write now to specify its a different one... Because it is
    {
        OnObjectTrashedAny = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnObjectTrashedAny?.Invoke(this, EventArgs.Empty);
        }
    }
}
