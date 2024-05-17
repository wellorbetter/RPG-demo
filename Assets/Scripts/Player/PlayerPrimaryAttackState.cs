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
        // 这个很不好控制，有时候拿到的是上一帧的，所以可能导致这个攻击方向比较混乱
        // 嗯，可能也不能这么说，就是这个xInput会在update里面被更新的，可能在进入enter的时候
        // 之前的这个update拿到的xInput不是我们想要的input
        // 改了效果感觉也不太好，有待商榷
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        // 这里是为了让玩家在某个时间段内无法进行操作
        // StartCoroutine 是一个协程，它的作用是可以让一个函数在中途暂停，然后在某个条件下继续执行
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
