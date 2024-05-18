using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // player.skill.sword好像不行
        SkillManager.instance.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.idleState);
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 如果当前的鼠标在人的右边，并且人物没有朝向右边 翻转
        if (player.transform.position.x < mousePosition.x && player.facingDir != 1)
        {
            player.Flip();
        }
        // 鼠标在左边，人没有朝向左边
        else if (player.transform.position.x > mousePosition.x && player.facingDir != -1)
        {
            player.Flip();
        }
    }
}
