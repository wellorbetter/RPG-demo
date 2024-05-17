using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _StateMachine, _animBoolName, _enemy)
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
        // �����˻�ִ�л��˵�setvelocity
        // ֻҪ���漰update������setvelocity�Ķ�Ҫ�޸�
        if (!enemy.isKnockback)
        {
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        }
        if (enemy.isWallDetected() || !enemy.isGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
