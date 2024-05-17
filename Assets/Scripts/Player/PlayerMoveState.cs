using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // 这里有一个bug就是，base是Gound状态，这个状态是可以切换到攻击状态的，但是呢
        // 切换到攻击状态之后，这一帧的update还没结束，就是我进入新状态的函数都执行玩了，我这里还要接着
        // 往下走，然后这里有一个setVelocity，会导致设置一个速度，所以会滑步
        if (this != player.stateMachine.currentState)
        {
            return;
        }
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
