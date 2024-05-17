using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            // 按了会反向，然后可能会满足下面的条件，所以这里要return
            return;
        }
        if (xInput != 0 && player.facingDir != xInput)
        {
            // 只要进入这个闲置状态，就会接着转变为airsate
            // 就是只要有输入，而且不是滑墙的那个方向，就会进入idel->air
            stateMachine.ChangeState(player.idleState);
        }
        // 滑墙结束了，就会进入闲置状态
        if (yInput < 0)
        {   // 滑墙快点
            player.SetVelocity(0, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.velocity.y * .7f);
        }
        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
