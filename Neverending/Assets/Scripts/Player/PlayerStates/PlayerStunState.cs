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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Debug.Log(playerData.stunTime);

        player.SetVelocityX(0f);

        if (Time.time >= startTime + playerData.stunTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
