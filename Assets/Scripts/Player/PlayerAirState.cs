using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        // 因为进行base更新然后再wallcheck，所以这里我们会先更新faceingdir，然后再进行wallcheck
        // 那么刚好某一帧，我进入了wallcheck，然后我就立马按了反方向键，然后这个时候先base update
        // 我就会先更新facingdir，然后再进行wallcheck，那么这个时候我就会发现我已经在墙上了，也就是说
        // 我就算是按了反方向键，我也还是在墙上
        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        // Movestate是继承自goundedstate的，但是空中我们应该也是要可以移动的
        // 所以这里我们的air状态也要可以移动
        if (xInput != 0)
        {
            // 这里有问题/？？
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }    
    }
}
