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
        // ��Ϊ����base����Ȼ����wallcheck�������������ǻ��ȸ���faceingdir��Ȼ���ٽ���wallcheck
        // ��ô�պ�ĳһ֡���ҽ�����wallcheck��Ȼ���Ҿ������˷��������Ȼ�����ʱ����base update
        // �Ҿͻ��ȸ���facingdir��Ȼ���ٽ���wallcheck����ô���ʱ���Ҿͻᷢ�����Ѿ���ǽ���ˣ�Ҳ����˵
        // �Ҿ����ǰ��˷����������Ҳ������ǽ��
        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        // Movestate�Ǽ̳���goundedstate�ģ����ǿ�������Ӧ��Ҳ��Ҫ�����ƶ���
        // �����������ǵ�air״̬ҲҪ�����ƶ�
        if (xInput != 0)
        {
            // ����������/����
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }    
    }
}
