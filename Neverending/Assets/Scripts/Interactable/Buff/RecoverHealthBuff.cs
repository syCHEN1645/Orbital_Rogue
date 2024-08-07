using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealthBuff : PermanentBuff
{
    [SerializeField]
    protected float amt = 10.0f;
    protected override void BuffEffect(PlayerData playerData)
    {
        Debug.Log("Health + " + amt);
        // increase player's health by "amt"
        playerData.RecoverHealth(amt);
        if (playerData.GetHealth() > playerData.GetMaxHealth()) {
            // prevent overflow
            playerData.SetHealth(playerData.GetMaxHealth());
        }
    }
}
