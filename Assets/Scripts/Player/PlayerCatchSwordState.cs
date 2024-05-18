using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;

        // ����׼��ʱ���࣬�ջص�ʱ��Ҳ���޸����ﳯ��
        // Ȼ�������������Ž��Ķ���
        if (player.transform.position.x < sword.position.x && player.facingDir != 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x > sword.position.x && player.facingDir != -1)
        {
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
