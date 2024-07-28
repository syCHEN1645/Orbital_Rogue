using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttackBuff : PermanentBuff
{
    [SerializeField]
    protected float attack = 0.5f;
    protected override void BuffEffect(PlayerData playerData)
    {
        playerData.AttackBoost(attack);
    }
}
