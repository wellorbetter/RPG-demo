using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }
    
    [Header("Move info")]
    public float moveSpeed = 12;
    public float jumpForce = 12;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set;}

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJump wallJump { get; private set; } 
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    #endregion

    // Awake会在Start之前调用
    protected override void Awake()
    {
        base.Awake();
        // 对于每个玩家而言，都会唯一对应一个状态机
        // 每个玩家会有很多不同的状态，比如Idle，Move，Attack等等
        // 但是这些状态在某个时刻只能有一个是当前状态，而且状态之间的切换就是状态机来控制的
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword"); 
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    // IEnumerator是一个接口，它的作用是可以让一个函数在中途暂停，然后在某个条件下继续执行
    // 这里的BusyFor就是一个协程，它的作用是让玩家在某个时间段内无法进行操作
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        // 这个函数的作用是等待_seconds秒之后再继续执行
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    // 专门的，因为游戏希望无论什么时候都可以进行冲刺
    private void CheckForDashInput()
    {
        
        // 之前的每秒减少的逻辑是写在player里面的
        // 现在写在了SkillManager管理的Dash_Skill实例dash里面
        // 只需要检查dash里面是否可用即可
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            // 因为是希望所有的时候都可以冲刺，所以攻击状态也可以冲刺
            // 但是我们希望用冲刺来规避伤害，但是攻击的时候是没有移动的，也就是说这个时候
            // 方向很可能面朝敌人，所以我们需要可以指定这个冲刺的方向，然后默认的话facingDir
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }
    
    
    
}
