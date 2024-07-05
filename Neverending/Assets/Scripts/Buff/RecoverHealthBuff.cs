using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealthBuff : PermanentBuff
{
    [SerializeField]
    protected float recovery = 10.0f;
    protected override void BuffEffect(Player player)
    {
        Debug.Log("Health + " + recovery);
        // increase player's health by "recovery"
        // player.Health.health += recovery;
    }
}
