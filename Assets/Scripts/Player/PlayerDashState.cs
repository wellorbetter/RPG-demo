using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 原地留一个clone
        SkillManager.instance.clone.CreateClone(player.transform);
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        // 空中冲刺遇到墙的时候需要立即转入wallslide状态
        // 当然在地上冲刺遇到墙是不用wallslide的
        /*if (!player.isGroundDetected() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }*/
        // 直接贴上去太僵硬了，算了
        if (stateTimer < 0)
        {
            // 这里冲刺结束之后会转向idel状态，但是呢，如果在空中，这里是不对的
            // 空中冲刺之后应该是要进入air状态的，所以这里需要给一个判断
            // 就是是不是在空中，这里可以写给GroundState，因为idel继承了它
            stateMachine.ChangeState(player.idleState);
        }
    }
}
