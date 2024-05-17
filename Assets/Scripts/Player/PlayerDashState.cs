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
        // ԭ����һ��clone
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
        // ���г������ǽ��ʱ����Ҫ����ת��wallslide״̬
        // ��Ȼ�ڵ��ϳ������ǽ�ǲ���wallslide��
        /*if (!player.isGroundDetected() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }*/
        // ֱ������ȥ̫��Ӳ�ˣ�����
        if (stateTimer < 0)
        {
            // �����̽���֮���ת��idel״̬�������أ�����ڿ��У������ǲ��Ե�
            // ���г��֮��Ӧ����Ҫ����air״̬�ģ�����������Ҫ��һ���ж�
            // �����ǲ����ڿ��У��������д��GroundState����Ϊidel�̳�����
            stateMachine.ChangeState(player.idleState);
        }
    }
}
