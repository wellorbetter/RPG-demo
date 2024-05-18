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
        // player.skill.sword������
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
        // �����ǰ��������˵��ұߣ���������û�г����ұ� ��ת
        if (player.transform.position.x < mousePosition.x && player.facingDir != 1)
        {
            player.Flip();
        }
        // �������ߣ���û�г������
        else if (player.transform.position.x > mousePosition.x && player.facingDir != -1)
        {
            player.Flip();
        }
    }
}
