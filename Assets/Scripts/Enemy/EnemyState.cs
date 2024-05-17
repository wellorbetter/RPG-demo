using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    protected string animBoolName;
    protected float stateTimer;
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName)
    {
        this.stateMachine = _StateMachine;
        this.enemyBase = _enemyBase;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationTrigger() => triggerCalled = true;

}
