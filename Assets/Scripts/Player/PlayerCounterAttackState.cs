using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ����Ҫ�Ӹ��ٶ�Ϊ0����Ȼ���ܸմ�move����������ٶȻ���û�䣬�ͻ�����
        player.SetZeroVelocity();
        // ��base������Զ�����CounterAttack��true��false �Լ�������ִ�� ���������ʱ��ֻ��counterAttackDuration��ô��
        stateTimer = player.counterAttackDuration;
        // �������SuccessfulCounterAttack����������ж��Ƿ�������״̬
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // ���빥����Χ
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        // ���˿��Ա�ѣ��
        foreach(var hit in colliders)
        {
            // Skeleton ��CanBeStunned �����������л�Skeleton��״̬
            if (hit.GetComponent<Enemy>() && hit.GetComponent<Enemy>().CanBeStunned())
            {
                // �����л���ѣ��״̬
                stateTimer = 10;
                // ִ��ѣ�ι�������
                player.anim.SetBool("SuccessfulCounterAttack", true);
            }
        }
        // ֮ǰ���óɵ�stateTimerΪ10�����Ǻ���triggerCalled����Ϊtrue
        // ��Ҫô��ִ�гɹ��ˣ����Ǹ�����ִ�����ˣ�Ҫô����û�ɹ�
        // û�ɹ�����Counter�����ƴ�����Ȼ��ת��idle
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
