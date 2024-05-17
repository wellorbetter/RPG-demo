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
        // 这里要加个速度为0，不然可能刚从move过来，这个速度还是没变，就滑步了
        player.SetZeroVelocity();
        // 在base里面会自动设置CounterAttack的true和false 以及动画的执行 这个动画的时间只有counterAttackDuration这么久
        stateTimer = player.counterAttackDuration;
        // 这里添加SuccessfulCounterAttack，后面进行判断是否进入这个状态
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // 进入攻击范围
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        // 敌人可以被眩晕
        foreach(var hit in colliders)
        {
            // Skeleton 的CanBeStunned 如果满足可以切换Skeleton的状态
            if (hit.GetComponent<Enemy>() && hit.GetComponent<Enemy>().CanBeStunned())
            {
                // 敌人切换到眩晕状态
                stateTimer = 10;
                // 执行眩晕攻击动画
                player.anim.SetBool("SuccessfulCounterAttack", true);
            }
        }
        // 之前设置成的stateTimer为10，就是好让triggerCalled设置为true
        // 即要么是执行成功了，把那个动画执行完了，要么就是没成功
        // 没成功就是Counter，蓄势待发，然后转到idle
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
