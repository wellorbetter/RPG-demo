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
        // ������һ��bug���ǣ�base��Gound״̬�����״̬�ǿ����л�������״̬�ģ�������
        // �л�������״̬֮����һ֡��update��û�����������ҽ�����״̬�ĺ�����ִ�����ˣ������ﻹҪ����
        // �����ߣ�Ȼ��������һ��setVelocity���ᵼ������һ���ٶȣ����ԻỬ��
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
