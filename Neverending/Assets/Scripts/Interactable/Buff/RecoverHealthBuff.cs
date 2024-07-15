using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealthBuff : PermanentBuff
{
    [SerializeField]
    protected float amt = 10.0f;
    protected override void BuffEffect(Player player)
    {
        Debug.Log("Health + " + amt);
        // increase player's health by "recovery"
        player.playerHealth.RecoverHealth(amt);
    }
}
