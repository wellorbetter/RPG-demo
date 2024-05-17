using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.aimSwordState);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }

        if (!player.isGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
        // 只有在地上才能进行攻击1、2、3
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.isGroundDetected())
        {
            stateMachine.ChangeState(player.primaryAttack);
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
