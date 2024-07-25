using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    ) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Debug.Log(playerData.StunTime);

        player.SetVelocity(0f);

        if (Time.time >= startTime + playerData.StunTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
