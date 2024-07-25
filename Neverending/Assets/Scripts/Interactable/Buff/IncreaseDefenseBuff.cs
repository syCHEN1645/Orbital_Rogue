using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefenseBuff : PermanentBuff
{
    [SerializeField]
    protected float defense = 0.3f;
    protected override void BuffEffect(PlayerData playerData)
    {
        // Debug.Log("Defense + " + defense);
        // increase player's defense by "amt"
        playerData.IncreaseDefense(defense);
    }
}
