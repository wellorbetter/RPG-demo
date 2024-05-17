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
            // ���˻ᷴ��Ȼ����ܻ������������������������Ҫreturn
            return;
        }
        if (xInput != 0 && player.facingDir != xInput)
        {
            // ֻҪ�����������״̬���ͻ����ת��Ϊairsate
            // ����ֻҪ�����룬���Ҳ��ǻ�ǽ���Ǹ����򣬾ͻ����idel->air
            stateMachine.ChangeState(player.idleState);
        }
        // ��ǽ�����ˣ��ͻ��������״̬
        if (yInput < 0)
        {   // ��ǽ���
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
