using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostBuff : TimedBuff
{
    [SerializeField]
    protected float attackBonus;
    AttackBoostBuff() {
        duration = 2.0f;
        attackBonus = 10.0f;
    }
    protected override IEnumerator BuffEffect(Player player)
    {
        // buff effect
        // Debug.Log("Attack+");
        
        yield return new WaitForSeconds(duration);
        // remove buff effect
        RemoveBuffEffect(player);
        // destroy buff
        Destroy(gameObject);
    }

    protected override void RemoveBuffEffect(Player player)
    {
        // Debug.Log("Attack-");
    }
}
