using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealthBuff : PermanentBuff
{
    [SerializeField]
    protected float amt;
    protected override void BuffEffect(PlayerData playerData)
    {
        Debug.Log("Health + " + amt);
        // increase player's health by "amt"
        float currentMaxHealth = playerData.GetMaxHealth();
        playerData.SetMaxHealth(currentMaxHealth + amt);
        playerData.RecoverHealth(amt);
    }
}
