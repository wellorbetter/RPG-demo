using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        /*xInput = 0;*/
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Choose attack direction

        float attackDir = player.facingDir;

        /*if (xInput != 0) attackDir = xInput;*/
        // ����ܲ��ÿ��ƣ���ʱ���õ�������һ֡�ģ����Կ��ܵ��������������Ƚϻ���
        // �ţ�����Ҳ������ô˵���������xInput����update���汻���µģ������ڽ���enter��ʱ��
        // ֮ǰ�����update�õ���xInput����������Ҫ��input
        // ����Ч���о�Ҳ��̫�ã��д���ȶ
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        // ������Ϊ���������ĳ��ʱ������޷����в���
        // StartCoroutine ��һ��Э�̣����������ǿ�����һ����������;��ͣ��Ȼ����ĳ�������¼���ִ��
        player.StartCoroutine("BusyFor", 0.15f);

        comboCounter++; 
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
