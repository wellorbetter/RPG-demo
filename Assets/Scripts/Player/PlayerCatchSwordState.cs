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
        // 同理，接剑的时候可能也是具有某个速度的
        // 这里设置为0
        player.SetZeroVelocity();
        sword = player.sword.transform;

        // 和瞄准的时候差不多，收回的时候也会修改人物朝向
        // 然后让人做出接着剑的动作
        if (player.transform.position.x < sword.position.x && player.facingDir != 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x > sword.position.x && player.facingDir != -1)
        {
            player.Flip();
        }
        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);
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
