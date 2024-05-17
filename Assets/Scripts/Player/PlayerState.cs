using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    // ��Ϊһ����һ��кܶ�״̬�����ҿ��ܲ�ͬ��һ��в�ͬ��״̬
    // ����������Ҫָ�����ĸ���ҵ�״̬�������Player����ָ�������
    // ���������stateMachine��Ҳ������ˣ���ΪPlayer�����Ѿ�����һ��stateMachine
    // ���������stateMachine��Ϊ�˷���PlayerState�����״̬֮����л�,�������ﻹ����Ҫ��
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    // �麯�������ǿ��Ա�������д�ĺ�����������Ը����Լ���������д�������
    public virtual void Enter()
    {
        // �����ʱ����Ҫ����ǰ�Ķ���״̬����Ϊtrue���뿪��ʱ������Ϊfalse
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        yInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void AnimationTrigger() => triggerCalled = true;
}
