using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostBuff : TimedBuff
{
    [SerializeField]
    protected float attackBonus;
    
    protected override IEnumerator BuffEffect(Player player)
    {
        // buff effect
        // Debug.Log("Attack+");
        // Debug.Log(player.GetPlayerData().baseAttack);
        player.playerData.Damage += attackBonus;
        // Debug.Log(player.GetPlayerData().baseAttack);
        yield return new WaitForSeconds(duration);
        // remove buff effect
        RemoveBuffEffect(player);
        // Debug.Log(player.GetPlayerData().baseAttack);
        // destroy buff
        Destroy(gameObject);
    }

    protected override void RemoveBuffEffect(Player player)
    {
        // Debug.Log("Attack-");
        player.playerData.Damage -= attackBonus;
    }
}
