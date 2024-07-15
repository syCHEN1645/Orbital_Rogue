using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefenseBuff : PermanentBuff
{
    [SerializeField]
    protected float defense = 10.0f;
    protected override void BuffEffect(Player player)
    {
        // Debug.Log("Defense + " + defense);
        // increase player's defense by "amt"
        player.playerHealth.IncreaseDefense(defense);
    }
}
