using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBoostBuff : TemporaryBuff
{
    [SerializeField]
    protected float defenseBonus;
    
    protected override IEnumerator BuffEffect(PlayerData playerData)
    {
        // buff effect
        // Debug.Log("Attack+");
        // Debug.Log(player.GetPlayerData().baseAttack);
        playerData.DefenseBoost(defenseBonus);
        // Debug.Log(player.GetPlayerData().baseAttack);
        yield return new WaitForSeconds(duration);
        // remove buff effect
        RemoveBuffEffect(playerData);
        // Debug.Log(player.GetPlayerData().baseAttack);
        // destroy buff
        Destroy(gameObject);
    }

    protected override void RemoveBuffEffect(PlayerData playerData)
    {
        // Debug.Log("Attack-");
        playerData.RecoverDefense(defenseBonus);
    }
}
